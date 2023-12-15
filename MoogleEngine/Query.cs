using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace MoogleEngine
{
    public class Query
    {

        
        
        public Dictionary<string, double> query;


        public Query(string consulta)
        {
            this.query = CalcularTDIDF(consulta);


        }


        public Dictionary<string, double> CopiarQuery()
        {
            Dictionary<string, double> copia = new Dictionary<string, double>(query);

            
            return copia;


        }

        public static List<string> CogerPalabras(string frase)
        {
            string regular = @"\W+";
            string[] partir = Regex.Split(frase, regular);
            List<string> palabras = new List<string>();

            foreach (string palabra in partir)
            {
                if(!string.IsNullOrEmpty(palabra))
                {
                    string minuscula = palabra.ToLower();

                    palabras.Add(minuscula);
                }
                else
                {
                    continue;
                }

               


            }

            return palabras;

        }

        public static Dictionary<string, double> Repeticiones(List<string> words)
        {
            Dictionary<string, double> frecuencias = new Dictionary<string, double>();

            foreach (string palabra in words)
            {
                if (frecuencias.ContainsKey(palabra))
                {

                    frecuencias[palabra]++;

                }
                else
                {
                    frecuencias.Add(palabra, 1);


                }

            }

            return frecuencias;

        }
        public static Dictionary<string, double> TF(string frase)
        {
            List<string> words = CogerPalabras(frase);
            Dictionary<string, double> frecuencias = Repeticiones(words);
            Dictionary<string, double> tfs = new Dictionary<string, double>();

            foreach (KeyValuePair<string, double> elementos in frecuencias)
            {
                double tf = (double)elementos.Value / words.Count;

                tfs.Add(elementos.Key, tf);



            }

            return tfs;


        }


        public static HashSet<string> CogerUna(List<string> palabras)
        {

            HashSet<string> unicas = new HashSet<string>();

            foreach (string palabra in palabras)
            {
                unicas.Add(palabra);

            }

            return unicas;

        }

        public static Dictionary<string, double> CopiarHash(HashSet<string> hash)
        {
            Dictionary<string, double> words = new Dictionary<string, double>();

            foreach (string j in hash)
            {
                words.Add(j, 0);

            }

            return words;

        }
        public static Dictionary<string, double> NumeroenDocumentos(string frase)
        {
            List<string> palabras = CogerPalabras(frase);
            HashSet<string> words = CogerUna(palabras);
            Dictionary<string, double> frecuencias = CopiarHash(words);
            string ruta = @"..\Content";
            string[] documentos = Directory.GetFiles(ruta, "*txt");




            foreach (string j in words)
            {
                for (int i = 0; i < documentos.Length; i++)
                {
                    string palabra = File.ReadAllText(documentos[i]).ToLower();

                    if (palabra.Contains(j))
                    {

                        frecuencias[j]++;


                    }
                    else
                    {
                        continue;
                    }




                }

            }

            return frecuencias;

        }
        public static Dictionary<string, double> IDF(string frase)
        {
            string ruta = @"..\Content";
            string[] documentos = Directory.GetFiles(ruta, "*txt");
            Dictionary<string, double> frecuencias = NumeroenDocumentos(frase);
            Dictionary<string, double> idfs = new Dictionary<string, double>();
            double logT = Math.Log(documentos.Length);

            foreach (KeyValuePair<string, double> elemento in frecuencias)
            {


                
                if (elemento.Value == 0)
                {

                    idfs.Add(elemento.Key, 0);
                }
                else if(elemento.Value == documentos.Length )
                {
                    idfs.Add(elemento.Key, 0.0000001);

                }
                else
                {
                    double idf = (double)  logT - Math.Log(elemento.Value);
                    idfs.Add(elemento.Key, idf);

                }






            }


            return idfs;
        }


        public static Dictionary<string, double> CalcularTDIDF(string frase)
        {
            Dictionary<string, double> tds = TF(frase);
            Dictionary<string, double> idfs = IDF(frase);
            Dictionary<string, double> tdidfs = new Dictionary<string, double>();

            foreach (KeyValuePair<string, double> elemento in tds)
            {
                double valor = (double)tds[elemento.Key] * idfs[elemento.Key];

                tdidfs.Add(elemento.Key, valor);



            }



            return tdidfs;



        }


        public void ImprimirDiccionario()
        {

            foreach (KeyValuePair<string, double> elemento in this.query)
            {
                Console.WriteLine("{0} : {1} ", elemento.Key, elemento.Value);


            }


        }





    }


}
