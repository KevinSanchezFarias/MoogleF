using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace MoogleEngine
{
    public class Documentos
    {
        public List<List<string>> textos;
        public Documentos()
        {

            this.textos = RecorrerDocumento();
            


        }

        public List<List<string>> CopiarDocumento()
        {
            List<List<string>> copia = new List<List<string>>(textos);

            return copia;
        }
        public int longitud
        {
            get { return textos.Count; }

        }


        public List<Dictionary<string, double>> Repeticiones()
        {
            List<Dictionary<string, double>> repeticiones = new List<Dictionary<string, double>>();

            foreach (List<string> texto in this.textos)
            {

                Dictionary<string, double> frecuencias = new Dictionary<string, double>();


                foreach (string palabra in texto)
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

                repeticiones.Add(frecuencias);

            }

            return repeticiones;
        }

        public static List<List<string>> RecorrerDocumento()
        {
            string ruta = @"..\Content";

            List<List<string>> textos = new List<List<string>>();

            string[] archivos = Directory.GetFiles(ruta, "*txt");
            string regular = @"\W+";

            for (int i = 0; i < archivos.Length; i++)
            {
                List<string> texto = new List<string>();

                string lineas = File.ReadAllText(archivos[i]);

                
                string[] partir = Regex.Split(lineas, regular);
                
                for (int j = 0; j < partir.Length; j++)
                {
                    string limpio = partir[j].ToLower();

                    if (!string.IsNullOrEmpty(limpio))
                    {
                       
                        texto.Add(limpio);
                    }
                    else
                    {
                        continue;
                    }







                }
                
               
                

                textos.Add(texto);

            }

            return textos;
        }


        public int Index(int indice)
        {
            return this.textos[indice].Count;



        }


        public HashSet<string> Vocabulario()
        {

            HashSet<string> unicas = new HashSet<string>();

            
            foreach (List<string> unic in textos)
            {

                foreach ( string word in unic)
                {
                    unicas.Add(word);
                }
                
            }


            return unicas;


        }










        public void RecorrerLista()
        {

            foreach (List<string> texto in textos)
            {


                Console.WriteLine("texto");

                foreach (string texto2 in texto)
                {

                    Console.WriteLine(texto2);

                }


            }


        }

    }
}
