using System.Collections.Generic;
using System.Linq;

namespace DatabaseLibrary
{
    public class MyProductService
    {
        /*
        Tutaj nie jestem pewien tego parametru myProducts - w etapie 5 zadania jest napisane, żeby
        zaimplementować trzy metody z etapu 3, a tam jest tylko string, po którym wyszukujemy.
        No dobra, jeśli chcielibyśmy zrobić z jednym parametrem musielibyśmy mieć dostęp do całego zbioru
        MyProduct (który dodałem w MyProductServiceTest), tylko wtedy to jest wkładanie danych testowych
        do biblioteki :) Można by było jeszcze zrobić to jako metodę rozszerzającą w taki sposób:
        public static List<MyProduct> GetMyProductsByName(this List<MyProduct> myProducts, string namePart)
        Która opcja ma być? XD
        */
        public static List<MyProduct> GetMyProductsByName(string namePart, List<MyProduct> myProducts)
        {
                return (from myProduct in myProducts
                        where myProduct.Name.Contains(namePart)
                        select myProduct).ToList();
        }
    }
}
