using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AmiaAbiturient
{

    public static class Methods<T>
    {
        public static bool RemoveLowerAddHigher(List<T> list, T deleted, T added)
        {
            if (list.Contains(deleted))
            {
                list.Remove(deleted);
                list.Add(added);
                return true;
            }
            return false;
        }
    }

    public class Abiturient : IComparable<Abiturient>
    {

        public int Id { get; set; }
        public string FIO { get; set; }
        public int Sex { get; set; }
        public int Lgota { get; set; }
        public short FirstCT { get; set; }
        public short SecondCT { get; set; }
        public short RusCT { get; set; }
        public short BallAttestat { get; set; }
        public int Result { get; set; }
        public int[] Specialities { get; set; }
        public short FirstProfBallAtt { get; set; }
        public short SecondProfBallAtt { get; set; }
        public short RusBelSum { get; set; }
        public bool Ideas100ForRB { get; set; }
        public bool KindHearth { get; set; }
        public bool Sotr { get; set; }
        public bool PK { get; set; }
        public bool MOOP { get; set; }
        public Abiturient() { }

        public int CompareTo(Abiturient a)
        {
            return a.Result - Result;
        }

        public static int CompareArrayLists(ArrayList al1, ArrayList al2)
        {
            for (int i = 0; i < al1.Count; i++)
            {
                if (Convert.ToInt32(al1[i]) > Convert.ToInt32(al2[i]))
                {
                    return 1;
                }
                else if (Convert.ToInt32(al1[i]) < Convert.ToInt32(al2[i]))
                {
                    return -1;
                }
            }
            return 0;
        }

        public static Abiturient FindAbiturientWithLowerLgots(List<Abiturient> abs)
        {
            if (abs.Count == 1)
            {
                return abs[0];
            }
            else
            {
                List<Abiturient> abiturients = abs.OrderBy(a => a.Lgota).ToList();
                List<Abiturient> abiturientsMinLgots = abiturients.Where(a => a.Lgota == abiturients[0].Lgota).ToList();
                Abiturient min = null;
                if (abiturientsMinLgots.Count > 1)
                {
                    min = Abiturient.FindAbiturientWithLowerBalls(abiturientsMinLgots, abiturientsMinLgots.FirstOrDefault().Lgota);
                }
                else
                {
                    min = abiturients[0];
                }
                return min;
            }
        }

        public static int CompareWhenEqualAmount(Abiturient ab1, Abiturient ab2) // преимущественное право при зачислении по п.п. 24, 26 Правил
        {
            ArrayList arrayList1 = null;
            ArrayList arrayList2 = null;
            if (ab1.Lgota >= 7)
            {
                arrayList1 = new ArrayList()
                {
                    ab1.FirstProfBallAtt,
                    ab1.SecondProfBallAtt,
                    ab1.Ideas100ForRB,
                    ab1.KindHearth,
                    ab1.BallAttestat,
                    ab1.RusBelSum,
                    ab1.MOOP,
                    ab1.FirstCT,
                    ab1.SecondCT,
                };
                arrayList2 = new ArrayList()
                {
                    ab2.FirstProfBallAtt,
                    ab2.SecondProfBallAtt,
                    ab2.Ideas100ForRB,
                    ab2.KindHearth,
                    ab2.BallAttestat,
                    ab2.RusBelSum,
                    ab2.MOOP,
                    ab2.FirstCT,
                    ab2.SecondCT,
                };
            }
            else if (ab1.Lgota > 0 && ab1.Lgota < 7)
            {
                arrayList1 = new ArrayList()
                {
                    ab1.FirstCT,
                    ab1.SecondCT,
                    ab1.FirstProfBallAtt,
                    ab1.SecondProfBallAtt,
                    ab1.Ideas100ForRB,
                    ab1.KindHearth,
                    ab1.BallAttestat,
                    ab1.PK,
                    ab1.MOOP
                };
                arrayList2 = new ArrayList()
                {
                    ab2.FirstCT,
                    ab2.SecondCT,
                    ab2.FirstProfBallAtt,
                    ab2.SecondProfBallAtt,
                    ab2.Ideas100ForRB,
                    ab2.KindHearth,
                    ab2.BallAttestat,
                    ab2.PK,
                    ab2.MOOP
                };
            }
            else
            {
                arrayList1 = new ArrayList()
                {
                    ab1.FirstCT,
                    ab1.SecondCT,
                    ab1.FirstProfBallAtt,
                    ab1.SecondProfBallAtt,
                    ab1.Ideas100ForRB,
                    ab1.KindHearth,
                    ab1.BallAttestat,
                    ab1.Sotr,
                    ab1.PK,
                    ab1.MOOP
                };
                arrayList2 = new ArrayList()
                {
                    ab2.FirstCT,
                    ab2.SecondCT,
                    ab2.FirstProfBallAtt,
                    ab2.SecondProfBallAtt,
                    ab2.Ideas100ForRB,
                    ab2.KindHearth,
                    ab2.BallAttestat,
                    ab2.Sotr,
                    ab2.PK,
                    ab2.MOOP
                };
            }
            return CompareArrayLists(arrayList1, arrayList2);
        }
        public static Abiturient FindAbiturientWithLowerBalls(List<Abiturient> abs, int lgota)
        {
            Dictionary<int, ArrayList> abiturients = new Dictionary<int, ArrayList>();
            abs = abs.Where(a => a.Lgota == lgota).OrderBy(a => a.Lgota).ThenBy(a => a.Result).ToList();
            Abiturient min = abs?.FirstOrDefault();
            if (abs.Count > 0)
            {
                for (int i = 0; i < abs.Count; i++)
                {
                    ArrayList arrayList = new ArrayList();
                    if (lgota >= 7)
                    {
                        arrayList = new ArrayList()
                    {
                        abs[i].FirstProfBallAtt,
                        abs[i].SecondProfBallAtt,
                        abs[i].Ideas100ForRB,
                        abs[i].KindHearth,
                        abs[i].BallAttestat,
                        abs[i].RusBelSum,
                        abs[i].MOOP,
                        abs[i].FirstCT,
                        abs[i].SecondCT,
                    };
                    }
                    else if (lgota > 0 && lgota < 7)
                    {
                        arrayList = new ArrayList()
                    {
                        abs[i].FirstCT,
                        abs[i].SecondCT,
                        abs[i].FirstProfBallAtt,
                        abs[i].SecondProfBallAtt,
                        abs[i].Ideas100ForRB,
                        abs[i].KindHearth,
                        abs[i].BallAttestat,
                        abs[i].PK,
                        abs[i].MOOP
                    };
                    }
                    else
                    {
                        arrayList = new ArrayList()
                    {
                        abs[i].FirstCT,
                        abs[i].SecondCT,
                        abs[i].FirstProfBallAtt,
                        abs[i].SecondProfBallAtt,
                        abs[i].Ideas100ForRB,
                        abs[i].KindHearth,
                        abs[i].BallAttestat,
                        abs[i].Sotr,
                        abs[i].PK,
                        abs[i].MOOP
                    };
                    }
                    abiturients[abs[i].Id] = arrayList;
                }
                ArrayList listToCompareMin = abiturients?[min.Id];

                foreach (var al in abiturients)
                {
                    if (al.Key != min.Id)
                    {
                        int res = CompareArrayLists(al.Value, listToCompareMin);
                        if (res == 1)
                        {
                            continue;
                        }
                        else if (res == -1)
                        {
                            listToCompareMin = al.Value;
                            min = abs.FirstOrDefault(a => a.Id == al.Key);
                        }
                        else
                        {
                            min = null;
                            Console.WriteLine("Есть абитуриенты с равными баллами при определении преимущественного права на зачисление при равном количестве баллов!");
                            foreach (var ab in abs)
                            {
                                Console.WriteLine(ab.FIO + " " + ab.Result);
                            }
                        }
                    }
                }
            }
            return min;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<int, List<int>> lgots = new Dictionary<int, List<int>>
            {
                [9] = new List<int> { 9999 },
                [8] = new List<int> { 9999 },
                [7] = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 13, 14 },
                [6] = new List<int> { 6, 11, 13 },
                [5] = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 13, 14 },
                [4] = new List<int> { 9999 },
                [3] = new List<int> { 3, 6, 11, 13 },
                [2] = new List<int> { 9999 },
                [1] = new List<int> { 9999 },
            };

            Dictionary<int, double> capacityLgots = new Dictionary<int, double>
            {
                [9] = 1,
                [8] = 1,
                [7] = 0.3,
                [6] = 0.3,
                [5] = 0.3,
                [4] = 0.3,
                [3] = 0.3,
                [2] = 0.1,
                [1] = 1.0,
            };

            List<Abiturient> ab = new List<Abiturient>
            {
                new Abiturient
                {
                    Id = 1,
                    FIO = "Иванов Иван Иванович",
                    Sex = 1,
                    Lgota = 8,
                    FirstCT = 98,
                    SecondCT = 100,
                    BallAttestat = 98,
                    Result = 297,
                    Specialities = new int[]{ 1, 2 },
                    FirstProfBallAtt = 10,
                    SecondProfBallAtt = 10,
                    RusBelSum = 14,
                    Ideas100ForRB = true,
                    KindHearth = true,
                    MOOP = true,
                },
                new Abiturient
                {
                    Id = 2,
                    FIO = "Петров Петр Петрович",
                    Sex = 1,
                    Lgota = 8,
                    FirstCT = 98,
                    SecondCT = 100,
                    BallAttestat = 98,
                    Result = 297,
                    Specialities = new int[]{ 1, 2 },
                    FirstProfBallAtt = 10,
                    SecondProfBallAtt = 9,
                    RusBelSum = 14,
                    Ideas100ForRB = true,
                    KindHearth = true,
                    MOOP = true,
                },
                new Abiturient
                {
                    Id = 3,
                    FIO = "Сидоров Сидор Сидорович",
                    Sex = 1,
                    Lgota = 7,
                    FirstCT = 97,
                    SecondCT = 100,
                    BallAttestat = 99,
                    Result = 297,
                    Specialities = new int[]{ 1, 2 },
                    FirstProfBallAtt = 8,
                    SecondProfBallAtt = 8,
                    RusBelSum = 14,
                    Ideas100ForRB = true,
                    KindHearth = true,
                    MOOP = true,
                },
                new Abiturient
                {
                    Id = 4,
                    FIO = "Степанов Степан Степанович",
                    Sex = 1,
                    Lgota = 1,
                    FirstCT = 96,
                    SecondCT = 100,
                    BallAttestat = 100,
                    Result = 297,
                    Specialities = new int[]{ 1, 2 },
                    FirstProfBallAtt = 8,
                    SecondProfBallAtt = 8,
                    RusBelSum = 14,
                    Ideas100ForRB = true,
                    KindHearth = true,
                    MOOP = true,
                },
                new Abiturient
                {
                    Id = 5,
                    FIO = "Макаров Макар Макарович",
                    Sex = 1,
                    Lgota = 2,
                    FirstCT = 97,
                    SecondCT = 100,
                    BallAttestat = 99,
                    Result = 297,
                    Specialities = new int[]{ 1, 2 },
                    FirstProfBallAtt = 8,
                    SecondProfBallAtt = 8,
                    RusBelSum = 14,
                    Ideas100ForRB = true,
                    KindHearth = true,
                    MOOP = true,
                },
            };

            ab.Sort();

            List<Abiturient> failed = new List<Abiturient>();
            List<int> success = new List<int>();

            List<Abiturient> sp1 = new List<Abiturient>(2);
            List<Abiturient> sp2 = new List<Abiturient>(2);
            Dictionary<string, List<Abiturient>> abiturients = new Dictionary<string, List<Abiturient>>()
            {
                ["sp1"] = sp1,
                ["sp2"] = sp2,
            };

            //Abiturient a = Abiturient.FindAbiturientWithLowerBalls(ab.Where(a => a.Lgota == 0).ToList(), 0);
            //Console.WriteLine(a?.FIO);


            List<Abiturient> abiturientsLgota = ab.Where(a => a.Lgota != 0).OrderByDescending(a => a.Lgota).ToList();

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < abiturientsLgota.Count; j++)
                {
                    int sp = abiturientsLgota[j].Specialities[i]; // id специальности
                    int lgota = abiturientsLgota[j].Lgota; // id льготы
                    if (sp != 0 && (lgots[lgota].Contains(sp) || lgots[lgota].Contains(9999))) // если специальность не равна 0 и льгота распространияется на указанную специальность и
                    {
                        int capacity = 0;
                        int capacityLgot = (int)Math.Round(abiturients["sp" + sp].Count + (abiturients["sp" + sp].Capacity * capacityLgots[lgota]));
                        if (capacityLgot <= abiturients["sp" + sp].Capacity)
                        {
                            capacity = capacityLgot;
                        }
                        else
                        {
                            capacity = abiturients["sp" + sp].Capacity;
                        }
                        if (abiturients["sp" + sp].Count < capacity) // если количество абитуриетнов на указанную специальность  меньше количества выделенных мест и
                        {
                            if (!success.Contains(abiturientsLgota[j].Id)) // если текущий абитуриент еще не зачислен
                            {
                                abiturients["sp" + sp].Add(abiturientsLgota[j]); // добавляем абитуриента на указанную им специальность
                                success.Add(abiturientsLgota[j].Id); // добавляем его идентификатор в лист зачисленных абитуриентов
                            }
                        }
                        else
                        {
                            Abiturient abiturientMinLgota = Abiturient.FindAbiturientWithLowerLgots(abiturients["sp" + sp]);

                            if (abiturientsLgota[j].Lgota > abiturientMinLgota?.Lgota)
                            {
                                Methods<Abiturient>.RemoveLowerAddHigher(abiturients["sp" + sp], abiturientMinLgota, abiturientsLgota[j]); // заменяем абитуриента с меньшим баллом в списке зачисленных на абитуриента с высшим баллом
                                Methods<int>.RemoveLowerAddHigher(success, abiturientMinLgota.Id, abiturientsLgota[j].Id); // заменяем id абитуриента с меньшим баллом на id абитуриента с большим баллом в списке зачисленных
                            }
                            else if (abiturientsLgota[j].Lgota == abiturientMinLgota?.Lgota)
                            {
                                int resCompareAbiturientLgots = Abiturient.CompareWhenEqualAmount(abiturientsLgota[j], abiturientMinLgota);
                                if (resCompareAbiturientLgots == 1)
                                {
                                    Methods<Abiturient>.RemoveLowerAddHigher(abiturients["sp" + sp], abiturientMinLgota, abiturientsLgota[j]); // заменяем абитуриента с меньшим баллом в списке зачисленных на абитуриента с высшим баллом
                                    Methods<int>.RemoveLowerAddHigher(success, abiturientMinLgota.Id, abiturientsLgota[j].Id); // заменяем id абитуриента с меньшим баллом на id абитуриента с большим баллом в списке зачисленных
                                }
                                else if (resCompareAbiturientLgots == 0)
                                {
                                    Console.WriteLine("Необходимо участие члена приемной комиссии - равные баллы при зачислении на специальность " + sp);
                                    Console.WriteLine(abiturientsLgota[j].FIO + " " + abiturientsLgota[j].FirstProfBallAtt + " --- " + abiturientMinLgota.FIO + " - " + abiturientMinLgota.FirstProfBallAtt);
                                    Console.Read();
                                }
                            }
                        }
                    }
                }
            }



            for (int x = 0; x < success.Count; x++)
            {
                Abiturient a = ab.FirstOrDefault(a => a.Id == success[x]);
                ab.Remove(a);
            }

            for (int x = 0; x < ab.Count; x++)
            {
                ab[x].Lgota = 0;
            }


            //Abiturient abiturientProlet = ab.Where(a => a.Id == 4).FirstOrDefault();
            //Console.WriteLine("Льгота после пролета со льготами: " + abiturientProlet.FIO + " " + abiturientProlet.Lgota);
            //зачисление на общих основаниях

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < ab.Count; j++)
                {
                    int sp = ab[j].Specialities[i]; // id специальности
                    if (sp != 0)
                    {
                        if (abiturients["sp" + sp].Count < abiturients["sp" + sp].Capacity) // если количество абитуриетнов на указанную специальность  меньше количества выделенных мест и
                        {
                            if (!success.Contains(ab[j].Id)) // если текущий абитуриент еще не зачислен
                            {
                                abiturients["sp" + sp].Add(ab[j]); // добавляем абитуриента на указанную им специальность
                                success.Add(ab[j].Id); // добавляем его идентификатор в лист зачисленных абитуриентов
                            }
                        }
                        else
                        {
                            Abiturient abiturientMinBalls = Abiturient.FindAbiturientWithLowerBalls(abiturients["sp" + sp], 0);
                            if (ab[j].Result > abiturientMinBalls?.Result)
                            {
                                Methods<Abiturient>.RemoveLowerAddHigher(abiturients["sp" + sp], abiturientMinBalls, ab[j]); // заменяем абитуриента с меньшим баллом в списке зачисленных на абитуриента с высшим баллом
                                Methods<int>.RemoveLowerAddHigher(success, abiturientMinBalls.Id, ab[j].Id); // заменяем id абитуриента с меньшим баллом на id абитуриента с большим баллом в списке зачисленных
                            }
                            else if (ab[j].Result == abiturientMinBalls?.Result)
                            {
                                int resCompareAbiturient = Abiturient.CompareWhenEqualAmount(ab[j], abiturientMinBalls);
                                if (resCompareAbiturient == 1)
                                {
                                    Methods<Abiturient>.RemoveLowerAddHigher(abiturients["sp" + sp], abiturientMinBalls, ab[j]); // заменяем абитуриента с меньшим баллом в списке зачисленных на абитуриента с высшим баллом
                                    Methods<int>.RemoveLowerAddHigher(success, abiturientMinBalls.Id, ab[j].Id); // заменяем id абитуриента с меньшим баллом на id абитуриента с большим баллом в списке зачисленных
                                }
                                else if (resCompareAbiturient == 0)
                                {
                                    Console.WriteLine("Необходимо участие члена приемной комиссии - равные баллы при зачислении на специальность " + sp);
                                    Console.WriteLine(ab[j].FIO + " " + ab[j].Result + " --- " + abiturientMinBalls?.FIO + " - " + abiturientMinBalls?.Result);
                                    Console.Read();
                                }
                            }
                        }
                    }
                    else
                    {
                        if (!success.Contains(ab[j].Id))
                        {
                            failed.Add(ab[j]);
                        }
                    }
                }
            }

            Console.WriteLine("Spec1");
            for (int i = 0; i < abiturients["sp1"].Count; i++)
            {
                Console.WriteLine(abiturients["sp1"][i].Id + " - " + abiturients["sp1"][i].Result);
            }
            Console.WriteLine(" ----------------------------- ");
            Console.WriteLine("Spec2");
            for (int i = 0; i < abiturients["sp2"].Count; i++)
            {
                Console.WriteLine(abiturients["sp2"][i].Id + " - " + abiturients["sp2"][i].Result);
            }


            Console.WriteLine(" ----------------------------- ");
            Console.WriteLine("Success");
            for (int i = 0; i < success.Count; i++)
            {
                Console.WriteLine(success[i]);
            }

            //Console.WriteLine(" ----------------------------- ");
            //Console.WriteLine("ab");
            //for (int i = 0; i < ab.Count; i++)
            //{
            //    Console.WriteLine(ab[i].FIO);
            //}

            //Console.WriteLine(" ----------------------------- ");
            //Console.WriteLine("Failed");
            //for (int i = 0; i < failed.Count; i++)
            //{
            //    Console.WriteLine(failed[i].Id);
            //}

            //преимущественное право на зачисление при равном количестве баллов среди льготников
            //ArrayList arrayList1 = new ArrayList() { 9, 8, 7, 7, true, true, 85 };
            //ArrayList arrayList2 = new ArrayList() { 9, 8, 7, 7, true, true, 85 };

            //Console.WriteLine(Abiturient.CompareArrayLists(arrayList1, arrayList2));

            Console.ReadKey();
        }
    }
}