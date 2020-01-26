using System;
using System.Collections.Generic;

namespace GrafFeladat_CSharp
{
    /// <summary>
    /// Irányítatlan, egyszeres gráf.
    /// </summary>
    class Graf
    {
        int csucsokSzama;
        /// <summary>
        /// A gráf élei.
        /// Ha a lista tartalmaz egy(A, B) élt, akkor tartalmaznia kell
        /// a(B, A) vissza irányú élt is.
        /// </summary>
        readonly List<El> elek = new List<El>();
        /// <summary>
        /// A gráf csúcsai.
        /// A gráf létrehozása után új csúcsot nem lehet felvenni.
        /// </summary>
        readonly List<Csucs> csucsok = new List<Csucs>();

        /// <summary>
        /// Létehoz egy úgy, N pontú gráfot, élek nélkül.
        /// </summary>
        /// <param name="csucsok">A gráf csúcsainak száma</param>
        public Graf(int csucsok)
        {
            this.csucsokSzama = csucsok;

            // Minden csúcsnak hozzunk létre egy új objektumot
            for (int i = 0; i < csucsok; i++)
            {
                this.csucsok.Add(new Csucs(i));
            }
        }

        //szélesség
        public Graf(int kezdopont)
        {
            HashSet<int> bejart = new HashSet<int>();

            Queue<int> kovetkezok = new Queue<int>();
            kovetkezok.Add(kezdopont);
            bejart.Add(kezdopont);

            foreach (var k in kovetkezok)
	        {
                k = kovetkezok.Dequeue();

                Console.WriteLine(this.csucsok[k]);

                foreach (El el in this.elek)
	            {
                    if((el.Csucs1 == k) && (!bejart.Contains(el.Csucs2)))
                    {
                        kovetkezok.Enqueue(el.Csucs2);
                        bejart.Add(el.Csucs2);
                    }
	            }
	        }

            
        }

        //mélység 1.
        public Graf(int kezdopont)
        {
            HashSet<int> bejart = new HashSet<int>();
            
            Stack<int> kovetkezok = new Stack<int>();
            kovetkezok.Push(kezdopont);
            bejart.Add(kezdopont());

            foreach (var k in kovetkezok)
	        {
                k = kovetkezok.Pop();
                
                Console.WriteLine(this.csucsok[k]);

                foreach (El el in elek)
	            {
                    if((el.Csucs1 == k) && (!bejart.Contains(el.Csucs2)))
                        {
                            kovetkezok.Pop(el.Csucs2);
                            bejart.Add(el.Csucs2);
                        }
	            }
	        }   
        }

        //összefüggőség
        public Graf(bool e)
        {
            HashSet<int> bejart = new HashSet<int>();

            Queue<int> kovetkezok = new Queue<int>();
            kovetkezok.Add(0);
            bejart.Add(0);

            foreach (var k in kovetkezok)
	        {
                k = kovetkezok.Enqueue();

                foreach (El el in elek)
	            {
                    if((el.Csucs1 == k) && (!bejart.Contains(el.Csucs2)))
                        {
                            kovetkezok.Enqueue(el.Csucs2);
                            bejart.Add(el.Csucs2);
                        }
	            }
	        }

            if(bejart.Count == this.csucsokSzama)
                {
                    return true;
                }
                else
                {
                    return false;
                }
        }

        //feszítőfa
        public Graf Feszitofa()
        {
            Graf fa = new Graf(this.csucsokSzama);

            HashSet<int> bejart = new HashSet<int>();
            Queue<int> kovetkezok = new Queue<int>();
            kovetkezok.Add(0);
            bejart.Add(0);

            foreach (var k in kovetkezok)
	        {
                k = kovetkezok.Enqueue();

                foreach (El el in elek)
	            {
                    if(el.Csucs1 == el)
                    {
                        if(!bejart.Contains(el.Csucs2))
                        {
                            bejart.Add(el.Csucs2);
                            kovetkezok.Enqueue(el.Csucs1);
                            fa.Add(el.Csucs1,el.Csucs2);
                        }
                    }
	            }
	        }
            return fa;
        }

        /// <summary>
        /// Hozzáad egy új élt a gráfhoz.
        /// Mindkét csúcsnak érvényesnek kell lennie:
        /// 0 &lt;= cs &lt; csúcsok száma.
        /// </summary>
        /// <param name="cs1">Az él egyik pontja</param>
        /// <param name="cs2">Az él másik pontja</param>
        public void Hozzaad(int cs1, int cs2)
        {
            if (cs1 < 0 || cs1 >= csucsokSzama ||
                cs2 < 0 || cs2 >= csucsokSzama)
            {
                throw new ArgumentOutOfRangeException("Hibas csucs index");
            }

            // Ha már szerepel az él, akkor nem kell felvenni
            foreach (var el in elek)
            {
                if (el.Csucs1 == cs1 && el.Csucs2 == cs2)
                {
                    return;
                }
            }

            elek.Add(new El(cs1, cs2));
            elek.Add(new El(cs2, cs1));
        }

        public override string ToString()
        {
            string str = "Csucsok:\n";
            foreach (var cs in csucsok)
            {
                str += cs + "\n";
            }
            str += "Elek:\n";
            foreach (var el in elek)
            {
                str += el + "\n";
            }
            return str;
        }
    }
}