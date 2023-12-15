using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MoogleEngine
{
    public class Matriz
    {
        public List<Dictionary<string, double>> tfidf;


        public Matriz()
        {

           
            Documentos textos = new Documentos();
            
            

            tfidf = CalcularTDIDFDoc(textos);
            


        }

        public List<Dictionary<string, double>> CopiarMatriz()
        {

            List<Dictionary<string, double>> copia = new  List<Dictionary<string, double>> (tfidf);

           

            return copia;
        }

        public int Tamaño
        {
            get { return tfidf.Count; }
        }
        public static  int Longitud(Matriz matriz)
        {
            return matriz.Tamaño;

        }
        public static List<Dictionary<string, double>> TFDoc(Documentos textos)
        {
             
             
            List<Dictionary<string, double>> repeticiones = textos.Repeticiones();
            
            
            List<Dictionary<string, double>> tfs = new List<Dictionary<string, double>>();


            int contador = -1; 

            
            foreach (Dictionary<string, double> valores in repeticiones)
            {
               
                Dictionary<string, double> resultado = new Dictionary<string, double>();
                
                contador++;
              
                foreach (KeyValuePair<string, double> palabras in valores)
                {
                    
                    
                    double tf = (double)palabras.Value / textos.Index(contador);





                    resultado.Add(palabras.Key, tf);



                }

                tfs.Add(resultado);

            }



            return tfs;



        }

        public static Dictionary<string, double> CopiarHash(HashSet<string> unicas)
        {
            
            Dictionary<string, double> copiahash = new Dictionary<string, double>();

            foreach (string unic in unicas)
            {
                copiahash.Add(unic , 0);

            }
            

            return copiahash;

        }
        public static Dictionary<string, double> NumeroenDocumentosTXT(Documentos textos)
        {

           

            HashSet<string> words = textos.Vocabulario();
            
            
           
            Dictionary<string, double> frecuencias =  CopiarHash(words);

           
            
            
            List<List<string>> copiatextos = textos.CopiarDocumento();



            foreach (List<string> texto in copiatextos)
            {
                HashSet<string> palabrasTexto = new HashSet<string>(texto);

                foreach (string palabra in words)
                {
                    if (palabrasTexto.Contains(palabra))
                    {
                        frecuencias[palabra]++;
                    }
                }
            }












            return frecuencias;
        }

                
        public static List<Dictionary<string, double>> CalcularTDIDFDoc(Documentos textos)
        {
            

            List<Dictionary<string, double>> tds = TFDoc(textos);
            Dictionary<string, double> idfs = IDFdoc(textos);
            List<Dictionary<string, double>> tdidfs = new List<Dictionary<string, double>>();


            for (int i = 0; i < tds.Count; i++)
            {
                

                Dictionary<string, double> valor = new Dictionary<string, double>();

                foreach (KeyValuePair<string, double> valores in tds[i])
                {
                     
                    double calculo = (double)tds[i][valores.Key] * idfs[valores.Key];


                    valor.Add(valores.Key, calculo);


                }

                tdidfs.Add(valor);


            }





            return tdidfs;



        }




       
        public static Dictionary<string, double> IDFdoc(Documentos textos)
        {

           
            string ruta = @"..\Content";
            string[] documentos = Directory.GetFiles(ruta, "*txt");
            Dictionary<string, double> repeticiones = NumeroenDocumentosTXT(textos);
            Dictionary<string, double> idfs = new Dictionary<string, double>();
             
            
            double logT = Math.Log(documentos.Length);

           
            
               

                

                foreach (KeyValuePair<string, double> elemento in repeticiones)
                {

                     

                     double idf =(double) logT -  Math.Log(elemento.Value);

                     idfs.Add(elemento.Key, idf);
                }




                 return idfs;   
     
            }


    }

}
