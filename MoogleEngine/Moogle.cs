namespace MoogleEngine;


public static class Moogle
{
    static Matriz matriz = new Matriz();


    public static SearchResult Query(string query) 
    {
       
      
       
        Query consulta = new Query(query);
       
        
        Puntuacion scores = new Puntuacion( matriz , consulta);
        
        Dictionary<string , double > orden = scores.OrdenarScores();
        
        Dictionary<(string, string), double> devolver = Puntuacion.Snippet(orden);

        SearchItem[] items = new SearchItem[orden.Count];
        int contador = 0;

        

        foreach (KeyValuePair< (string , string ) , double  > par in devolver )
        {

            items[contador]=new SearchItem(par.Key.Item1, par.Key.Item2 , (float)par.Value);
            contador++;
        }

             return new SearchResult(items, query);
    }
}

       

        

       
           
       
       
        

           
           
            
        



       


           
        

           
        

   
