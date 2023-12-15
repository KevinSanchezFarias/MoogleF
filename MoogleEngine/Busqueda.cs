using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MoogleEngine
{
    public class Puntuacion
    {
        public Dictionary<string, double> puntaje;

        public Puntuacion(Matriz matriz, Query query)
        {
            
            List<string> nombres = Nombres();
            List<double> scores = Scores(matriz, query);
            this.puntaje = Union(nombres, scores);


        }

          public static List<string> Nombres()
        {
            string ruta = @"..\Content";
            List<string> nombres = new List<string>();
            string[] rutas = Directory.GetFiles(ruta, "*txt");

            foreach (string archivo in rutas)
            {
                nombres.Add(Path.GetFileName(archivo));

            }

            
            return nombres;


        }
       
        public static List<double> Scores(Matriz matriz, Query query)
        {
            List<double> scores = new List<double>();
            Dictionary<string, double> copiaQuery = query.CopiarQuery();
            List<Dictionary<string, double>> copiarMatriz = matriz.CopiarMatriz();
            

            foreach (Dictionary<string, double> par1 in copiarMatriz)
            {
                double valor = SimilitudEntreCosenos(par1, copiaQuery);
                scores.Add(valor);

 
            }


           


            return scores;


        }
        
        public static Dictionary<string, double> Union(List<string> nombres, List<double> scores)
        {
            Dictionary<string, double> namescores = new Dictionary<string, double>();

            for (int i = 0; i < nombres.Count; i++)
            {

                namescores.Add(nombres[i], scores[i]);



            }

            return namescores;
        }

        public static Dictionary<(string, string), double> Snippet(Dictionary<string, double> scoresordenados)
        {
            Dictionary<(string, string), double> dicconsnippets = new Dictionary<(string, string), double>();



            foreach (KeyValuePair<string, double> par in scoresordenados)
            {
                string ruta = string.Format(@"..\Content\{0}", par.Key);
                string palabra = File.ReadAllText(ruta);
                int segundaPosicionPunto = palabra.IndexOf('.', palabra.IndexOf('.') + 1);
                string subpalabra = palabra.Substring(0, segundaPosicionPunto + 1).ToLower();
                dicconsnippets.Add((par.Key, subpalabra), par.Value);
            }

            return dicconsnippets;
        }

        public Dictionary<string, double> OrdenarScores()
        {
            List<double> scores = new List<double>();
            Dictionary<string, double> orden = new Dictionary<string, double>();


            foreach (KeyValuePair<string, double> par in this.puntaje)
            {
                scores.Add(par.Value);
                


            }


            scores.Sort();



            for (int i = scores.Count - 1; i >= 0; i--)
            {
                foreach (KeyValuePair<string, double> par in this.puntaje)
                {
                    if (scores[i] == par.Value)
                    {
                        if (orden.ContainsKey(par.Key))
                        {
                            continue;
                        }
                        
                    

                        orden.Add(par.Key, scores[i]);

                        break;
                    

                       




                    }


                }



            }

            return orden;



        }



        

       


       

        public static double SimilitudEntreCosenos(Dictionary<string, double> entrada, Dictionary<string, double> salida)
        {
            Dictionary<string, Tuple<double, double>> resultado = NewCollection(entrada, salida);

            double productoescalar = ProductoEscalar(resultado);
            double magnitud = ProductoMagnitudVectores(resultado);

            double similitud = productoescalar / magnitud;

            return similitud;







        }

        public static double ProductoEscalar(Dictionary<string, Tuple<double, double>> resultado)
        {
            double producto = 0;



            foreach (KeyValuePair<string, Tuple<double, double>> trio in resultado)
            {

                producto += (double)resultado[trio.Key].Item1 * resultado[trio.Key].Item2;




            }

            return producto;

        }



        public static double ProductoMagnitudVectores(Dictionary<string, Tuple<double, double>> resultado)
        {



            double magnitud = 0;
            double suma1 = 0;
            double suma2 = 0;


            foreach (KeyValuePair<string, Tuple<double, double>> trio in resultado)
            {

                suma1 += (double)Math.Pow(resultado[trio.Key].Item1, 2);
                suma2 += (double)Math.Pow(resultado[trio.Key].Item2, 2);


            }



            magnitud = (double)(Math.Sqrt(suma1) * Math.Sqrt(suma2));



            return magnitud;

        }





        

        public static Dictionary<string, Tuple<double, double>> NewCollection(Dictionary<string, double> entrada, Dictionary<string, double> salida)
        {
            Dictionary<string, Tuple<double, double>> resultado = new Dictionary<string, Tuple<double, double>>();

            foreach (KeyValuePair<string, double> par in entrada)
            {
                resultado[par.Key] = new Tuple<double, double>(par.Value, 0);



            }

            foreach (KeyValuePair<string, double> par in salida)
            {
                if (resultado.ContainsKey(par.Key))
                {
                    resultado[par.Key] = new Tuple<double, double>(resultado[par.Key].Item1, par.Value);


                }
                else
                {

                    resultado[par.Key] = new Tuple<double, double>(0, par.Value);
                }



            }

            return resultado;

        }

    }

}

