using SmartHotel.Clients.Core.Models;
using System.Collections.Generic;
using System;
using System.Globalization;

namespace SmartHotel.Clients.Core.Helpers
{
    public class DOSAOLIVARStaticValues
    {
        CultureInfo culture;
        string specifier;
        public DOSAOLIVARStaticValues()
        {
            // Use standard numeric format specifiers.
            specifier = "G";
            culture = CultureInfo.CreateSpecificCulture("en-US");
        }


        public List<FuncionModel> getFuncion()
        {
            List<FuncionModel> listaFuncion = new List<FuncionModel>();
            listaFuncion.Add(new FuncionModel() { des = "Insecticida", value = "0" });
            listaFuncion.Add(new FuncionModel() { des = "Bactericida", value = "1" });
            listaFuncion.Add(new FuncionModel() { des = "Fungicida", value = "2" });
            listaFuncion.Add(new FuncionModel() { des = "Repelentes", value = "3" });
            listaFuncion.Add(new FuncionModel() { des = "Atrayentes", value = "4" });
            listaFuncion.Add(new FuncionModel() { des = "Fitorreguladores", value = "5" });
            listaFuncion.Add(new FuncionModel() { des = "Acaricidas", value = "6" });
            listaFuncion.Add(new FuncionModel() { des = "Herbicidas", value = "7" });
            return listaFuncion;
        }

        public List<MateriaActivaModel> getMateriaActiva()
        {
            List<MateriaActivaModel> listaMateriaActiva = new List<MateriaActivaModel>();
            listaMateriaActiva.Add(new MateriaActivaModel() { des = "ACEITE DE PARAFINA", value = "0" });
            listaMateriaActiva.Add(new MateriaActivaModel() { des = "AZUFRE", value = "1" });
            listaMateriaActiva.Add(new MateriaActivaModel() { des = "BETACIFLUTRIN; DELTAMETRIN; DIMETOATO", value = "2" });
            listaMateriaActiva.Add(new MateriaActivaModel() { des = "DIMETOATO", value = "3" });
            listaMateriaActiva.Add(new MateriaActivaModel() { des = "FOSMET", value = "4" });
            listaMateriaActiva.Add(new MateriaActivaModel() { des = "ACETAMIPRID", value = "5" });
            listaMateriaActiva.Add(new MateriaActivaModel() { des = "ALFA CIPERMETRIN", value = "6" });
            listaMateriaActiva.Add(new MateriaActivaModel() { des = "BACILLUS THURINGIENSIS", value = "7" });
            listaMateriaActiva.Add(new MateriaActivaModel() { des = "BETACIFLUTRIN", value = "8" });
            listaMateriaActiva.Add(new MateriaActivaModel() { des = "CAOLIN", value = "9" });
            listaMateriaActiva.Add(new MateriaActivaModel() { des = "CARFENTRAZONA-ETIL", value = "10" });
            listaMateriaActiva.Add(new MateriaActivaModel() { des = "CIPERMETRIN", value = "11" });
            listaMateriaActiva.Add(new MateriaActivaModel() { des = "CLORPIRIFOS", value = "12" });
            listaMateriaActiva.Add(new MateriaActivaModel() { des = "DELTAMETRIN", value = "13" });
            listaMateriaActiva.Add(new MateriaActivaModel() { des = "DIFLUFENICAN", value = "14" });
            listaMateriaActiva.Add(new MateriaActivaModel() { des = "DIMETOATO", value = "15" });
            listaMateriaActiva.Add(new MateriaActivaModel() { des = "DODINA", value = "16" });
            listaMateriaActiva.Add(new MateriaActivaModel() { des = "ETEFON", value = "17" });
            listaMateriaActiva.Add(new MateriaActivaModel() { des = "FLAZASULFURON", value = "17" });
            listaMateriaActiva.Add(new MateriaActivaModel() { des = "FOSMET", value = "18" });
            listaMateriaActiva.Add(new MateriaActivaModel() { des = "GLIFOSATO", value = "19" });
            listaMateriaActiva.Add(new MateriaActivaModel() { des = "HIDROXIDO CUPRICO", value = "20" });
            listaMateriaActiva.Add(new MateriaActivaModel() { des = "IMIDACLOPRID", value = "21" });
            listaMateriaActiva.Add(new MateriaActivaModel() { des = "LAMBDA CIHALOTRIN", value = "22" });
            listaMateriaActiva.Add(new MateriaActivaModel() { des = "MANCOZEB", value = "23" });
            listaMateriaActiva.Add(new MateriaActivaModel() { des = "MCPA", value = "24" });
            listaMateriaActiva.Add(new MateriaActivaModel() { des = "OXICLORURO DE COBRE", value = "25" });
            listaMateriaActiva.Add(new MateriaActivaModel() { des = "OXIFLUORFEN", value = "26" });
            listaMateriaActiva.Add(new MateriaActivaModel() { des = "OXIDO CUPROSO", value = "27" });
            listaMateriaActiva.Add(new MateriaActivaModel() { des = "PIRIPROXIFEN", value = "28" });
            listaMateriaActiva.Add(new MateriaActivaModel() { des = "SULFATO CUPROCALCICO", value = "29" });
            listaMateriaActiva.Add(new MateriaActivaModel() { des = "TEBUCOZANOL", value = "30" });
            listaMateriaActiva.Add(new MateriaActivaModel() { des = "UREA + PPROTEINAS HIDROLIZADAS", value = "31" });
            return listaMateriaActiva;
        }

        public List<DensidadHojasModel> getDensidadHojas()
        {
            List<DensidadHojasModel> listaDensidadHojas = new List<DensidadHojasModel>();
            listaDensidadHojas.Add(new DensidadHojasModel() { des = "Baja", value = "0" });
            listaDensidadHojas.Add(new DensidadHojasModel() { des = "Normal", value = "1" });
            listaDensidadHojas.Add(new DensidadHojasModel() { des = "Alta", value = "2" });
            return listaDensidadHojas;
        }

        public List<SistemaCultivoModel> getSistemasCultivos()
        {
            List<SistemaCultivoModel> listaSistemasCultivos = new List<SistemaCultivoModel>();
            listaSistemasCultivos.Add(new SistemaCultivoModel() { des = "Olivar Tradicional", value = "0" });
            listaSistemasCultivos.Add(new SistemaCultivoModel() { des = "Olivar intensivo", value = "1" });
            listaSistemasCultivos.Add(new SistemaCultivoModel() { des = "Olivar superintensivo", value = "2" });
            return listaSistemasCultivos;
        }



        public List<MarcoPlantacionModel> getMarcoPlantacion()
        {
            List<MarcoPlantacionModel> listaMarcoPlantacion = new List<MarcoPlantacionModel>();
            listaMarcoPlantacion.Add(new MarcoPlantacionModel() { des = "Real/Rectangular", value = "0", photourl = "assets/images/dosaolivar/marco_plantacion/marco_rectangular.png" });
            listaMarcoPlantacion.Add(new MarcoPlantacionModel() { des = "Tresbolillo", value = "1", photourl = "assets/images/dosaolivar/marco_plantacion/tresbolillo.png" });
            listaMarcoPlantacion.Add(new MarcoPlantacionModel() { des = "Cinco de oros", value = "2", photourl = "assets/images/dosaolivar/marco_plantacion/marco_rectangular.png" });
            return listaMarcoPlantacion;
        }

        public List<string> getNumerosIntensivoA()
        {
            /*
             * Modificacion temporal
             *
             * 
             List<string> listaNumeros = new List<string>();
             listaNumeros.Add("3");
             listaNumeros.Add("4");
             listaNumeros.Add("5");
             return listaNumeros;
             */

            List<string> listaNumeros = new List<string>();
            listaNumeros.Add("1");
            listaNumeros.Add("2");
            listaNumeros.Add("3");
            listaNumeros.Add("4");
            listaNumeros.Add("5");
            listaNumeros.Add("6");
            listaNumeros.Add("7");
            listaNumeros.Add("8");
            listaNumeros.Add("9");
            listaNumeros.Add("10");
            listaNumeros.Add("11");
            listaNumeros.Add("12");
            listaNumeros.Add("13");
            listaNumeros.Add("14");
            return listaNumeros;
        }


        public List<string> getNumerosIntensivoS()
        {
           /* List<string> listaNumeros = new List<string>();
            listaNumeros.Add("1");
            listaNumeros.Add("2");
            listaNumeros.Add("3");
            return listaNumeros;*/


            List<string> listaNumeros = new List<string>();
            listaNumeros.Add("1");
            listaNumeros.Add("2");
            listaNumeros.Add("3");
            listaNumeros.Add("4");
            listaNumeros.Add("5");
            listaNumeros.Add("6");
            listaNumeros.Add("7");
            listaNumeros.Add("8");
            listaNumeros.Add("9");
            listaNumeros.Add("10");
            listaNumeros.Add("11");
            listaNumeros.Add("12");
            listaNumeros.Add("13");
            listaNumeros.Add("14");
            return listaNumeros;

        }

        public List<string> getNumerosTradicional()
        {
            /*  List<string> listaNumeros = new List<string>();
              listaNumeros.Add("8");
              listaNumeros.Add("9");
              listaNumeros.Add("10");
              listaNumeros.Add("11");
              listaNumeros.Add("12");
              listaNumeros.Add("13");
              listaNumeros.Add("14");*/

            List<string> listaNumeros = new List<string>();
            listaNumeros.Add("1");
            listaNumeros.Add("2");
            listaNumeros.Add("3");
            listaNumeros.Add("4");
            listaNumeros.Add("5");
            listaNumeros.Add("6");
            listaNumeros.Add("7");
            listaNumeros.Add("8");
            listaNumeros.Add("9");
            listaNumeros.Add("10");
            listaNumeros.Add("11");
            listaNumeros.Add("12");
            listaNumeros.Add("13");
            listaNumeros.Add("14");
            return listaNumeros;
        }

        public List<string> getNumeros()
        {
            List<string> listaNumeros = new List<string>();
            listaNumeros.Add("0");
            listaNumeros.Add("1");
            listaNumeros.Add("2");
            listaNumeros.Add("3");
            listaNumeros.Add("4");
            listaNumeros.Add("5");
            listaNumeros.Add("6");
            listaNumeros.Add("7");
            listaNumeros.Add("8");
            listaNumeros.Add("9");

            return listaNumeros;
        }




        public bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }

        public string getLocalidad(string str)
        {

            string toReturn = "";

            bool encontrado = false;
            bool finalizado = false;
            foreach (char c in str)
            {
                if (c == '(')
                {
                    encontrado = true;
                }
                else if (c == ')')
                {
                    finalizado = true;
                }
                else if (encontrado && !finalizado)
                {
                    toReturn += c;
                }
            }
            if (encontrado)
            {
                return toReturn;
            }
            else
            {
                return str;
            }

        }

        public string DoubleToEUString(double _valor)
        {
            string valor = _valor.ToString();
            CultureInfo cultureES = CultureInfo.CreateSpecificCulture("fr-FR");
            /* Console.WriteLine("double to eu: " + _valor);
             Console.WriteLine("double to eu: " + String.Format(cultureES, "{0:N2}", valor));
             Console.WriteLine("double to eu: " + _valor.ToString("N1", CultureInfo.CreateSpecificCulture("sv-SE")));
             Console.WriteLine("double to eu: " + _valor.ToString("N2",CultureInfo.CreateSpecificCulture("sv-SE"))); */
            // string temp = String.Format("{0:0.0#}", _valor);
            string temp = _valor.ToString("N2", CultureInfo.CreateSpecificCulture("sv-SE"));
            temp = temp.Replace(" ", ".");
            return temp;
            //return string.Format(cultureES, "{0:N2}", valor);
        }

        /// <summary>
        /// Calculo COPAS


        public double getDoubleCopaVCA(string _H, string _D1, string _D2)
        {
            Console.WriteLine("recibimos " + _H + " " + _D1 + " " + _D2 + " " + _D1.Length);
            //Console.WriteLine("recibimos1" + _H);
            double toReturn = 0.0;
            CultureInfo englishGBCulture = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentCulture = englishGBCulture;
            CultureInfo.DefaultThreadCurrentUICulture = englishGBCulture;
            System.Threading.Thread.CurrentThread.CurrentCulture = englishGBCulture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = englishGBCulture;
            try
            {

                double H = 0.0;
                double D1 = 0.0;
                double D2 = 0.0;
                string d1 = _D1.Replace(",", ".");
                string d2 = _D2.Replace(",", ".");
                string h = _H.Replace(",", ".");
                Console.WriteLine("recibimos " + h + " " + d1 + " " + d2);
                try
                {
                    if (_H.Length > 0 || _H != null)
                    {
                        H = Double.Parse(_H.Replace(",", "."));
                    }
                    if (_D1.Length > 0 || _D1 != null)
                    {
                        D1 = Double.Parse(_D1.Replace(",", "."));
                    }
                    if (_D2.Length > 0 || _D2 != null)
                    {
                        D2 = Double.Parse(_D2.Replace(",", "."));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error haciendo parsing double en getStringCopaVCA: " + ex.Message);
                }
                Console.WriteLine("recibimos " + H + " " + D1 + " " + D2);
                toReturn = (4.00 / 3.00) * Math.PI;
                toReturn = toReturn * ((H / 2.0) * (D1 / 2.0) * (D2 / 2.0));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("devolvemos " + "{0:N2}", toReturn);
            return toReturn; // return toReturn.ToString(specifier, culture);
        }

        public double getDoubleValue(string value)
        {
            double reply = 0.0;
            CultureInfo englishGBCulture = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentCulture = englishGBCulture;
            CultureInfo.DefaultThreadCurrentUICulture = englishGBCulture;
            System.Threading.Thread.CurrentThread.CurrentCulture = englishGBCulture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = englishGBCulture;
            try
            {

                string h = value.Replace(",", ".");
                try
                {
                    if (value.Length > 0 || value != null)
                    {
                        reply = Double.Parse(value.Replace(",", "."));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error haciendo parsing double en getDoubleValue: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return reply;
        }

        // Numero de arboles por hectarea 
        public double getNha(string _ha, string _a, string _s, string _tipo)
        {
            Console.WriteLine("calculando Nha DOUBLE para tipo de marco " + _tipo);
            double Nha = 0.0;
            if (_tipo == "0")
            {
                double ha = double.Parse(_ha);
                double a = double.Parse(_a);
                double s = double.Parse(_s);
                double St = a * s;
                //  Nha = (ha * 10000) / St;
                Nha = (10000) / St;
            }
            else if (_tipo == "1")
            {
                double ha = double.Parse(_ha);
                double a = double.Parse(_a);
                double s = double.Parse(_s);
                double St = (Math.Sqrt(Math.Pow(a, 2) - (Math.Pow(s, 2) / 4.0))) * s;
                //  Nha = (ha * 10000) / St;
                Nha = (10000) / St;
            }
            Console.WriteLine("resultado: " + Nha);
            return Nha;
        }


        public double getDoubleArbolesHectarea(string _ha, string _a, string _s, string _tipo)
        {
            Console.WriteLine("calculando Nha para tipo de marco " + _tipo);
            double Na = 0.0;
            if (_tipo == "0")
            {
                double ha = double.Parse(_ha);
                double a = double.Parse(_a);
                double s = double.Parse(_s);
                double St = a * s;
                //Na = (ha * 10000) / St;
                Na = 10000 / St;
                Console.WriteLine("valor St getDoubleArbolesHectarea marco normal " + St);
            }
            else if (_tipo == "1")
            {
                double ha = double.Parse(_ha);
                double a = double.Parse(_a);
                double s = double.Parse(_s);
                double St = (Math.Sqrt(Math.Pow(a, 2) - (Math.Pow(s, 2) / 4.0))) * s;
                Console.WriteLine("getDoubleArbolesHectarea marco tresbolillo " + St);
                //Na = (ha * 10000) / St;
                Na = 10000 / St;
            }
            Console.WriteLine("-- fin funcion getDoubleArbolesHectarea -- " + _tipo);
            return Na;
            // return String.Format(culture, "{0}", Convert.ToInt32(Na));
        }


        public double getIntArbolesHectarea(string _ha, string _a, string _s, string _tipo)
        {
            Console.WriteLine("calculando Nha para tipo de marco " + _tipo);
            double Na = 0.0;
            if (_tipo == "0")
            {
                double ha = double.Parse(_ha);
                double a = double.Parse(_a);
                double s = double.Parse(_s);
                double St = a * s;
                //Na = (ha * 10000) / St;
                Na = 10000 / St;
                Console.WriteLine("valor St getDoubleArbolesHectarea marco normal " + St);
            }
            else if (_tipo == "1")
            {
                double ha = double.Parse(_ha);
                double a = double.Parse(_a);
                double s = double.Parse(_s);
                double St = (Math.Sqrt(Math.Pow(a, 2) - (Math.Pow(s, 2) / 4.0))) * s;
                Console.WriteLine("getDoubleArbolesHectarea marco tresbolillo " + St);
                //Na = (ha * 10000) / St;
                Na = 10000 / St;
            }
            Console.WriteLine("-- fin funcion getDoubleArbolesHectarea -- " + _tipo);
            return Convert.ToInt32(Na);
            // return String.Format(culture, "{0}", Convert.ToInt32(Na));
        }

        public double getDoubleVolumenCopaHectarea(double _nha, string _vca)
        {
            Console.WriteLine("Averiguando Vha resultado de Nha =" + _nha + " X vca = " + _vca);
            double Vha = 0.0; // m3 / ha
            double vca = GetDouble(_vca, 0.0);
            Console.WriteLine("getDoubleVolumenCopaHectarea, Caculamos Vha multiplicando Nha =" + _nha + " X vca = " + vca);
            Vha = (_nha * vca);
            return Vha;
        }




        public double getDoubleVolumenCaldoHectarea(double _Vha, string _tipo_sistema_cultivo, string densidad_hojas_id)
        {
            Console.WriteLine("calculando Vca para sistema de cultivo  " + _tipo_sistema_cultivo);
            Console.WriteLine("calculando Vca para densidad hojas  " + densidad_hojas_id);
            double Vca = 0.0;
            double Vha = _Vha;
            Console.WriteLine("Vha:" + Vha);
            if (_tipo_sistema_cultivo == "1" || _tipo_sistema_cultivo == "2")
            {
                Vca = Vha * 0.12;
                Console.WriteLine("getDoubleVolumenCaldoHectarea " + Vca);
            }
            else if (_tipo_sistema_cultivo == "0")
            {
                double constante = 0.10;
                if (densidad_hojas_id == "0")
                {
                    constante = 0.85;
                }
                else if (densidad_hojas_id == "2")
                {
                    constante = 1.15;
                }
                Console.WriteLine("La constante para sistema cultivo tradicional es:" + constante);
                Vca = Vha * constante * 0.10;
            }
            Console.WriteLine("-- getStringVolumenCaldoHectarea" + Vca);
            return Vca;
        }


        //  Cálculo del caudal de aplicación.
        public double getDoubleCalculoCaudalAplicacion(double _Vca, string _tipo_marco, string _a, string _s, double _velocidad)
        {
            Console.WriteLine("getDoubleCalculoCaudalAplicacion recibido " + _Vca + " _tipo_marco" + " " + _a + " " + _s + " " + _velocidad + " tipo marco:" + _tipo_marco);
            double Qt = 0.0;
            Console.WriteLine("getDoubleCalculoCaudalAplicacion recibido " + _Vca + " _tipo_marco" + " " + _a + " " + _s + " " + _velocidad);
            double a = double.Parse(_a);
            double s = double.Parse(_s);

            if (_tipo_marco == "1")
            {
                a = Math.Sqrt(Math.Pow(a, 2) - (Math.Pow(s, 2) / 4.0));
            }
            Qt = ((_Vca * _velocidad) / 600) * a;

            return Qt;
        }


        public static double GetDouble(string value, double defaultValue)
        {
            double result;

            // Try parsing in the current culture
            if (!double.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out result) &&
                // Then try in US english
                !double.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out result) &&
                // Then in neutral language
                !double.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out result))
            {
                result = defaultValue;
            }
            return result;
        }



    }
}


/*
 *
 *
 *
 *
 *

     public string getStringArbolesHectarea(string _ha, string _a, string _s, string _tipo)
        {
            Console.WriteLine("calculando Nha para tipo de marco " + _tipo);
            double Na = 0.0;
            if (_tipo == "0")
            {
                double ha = double.Parse(_ha);
                double a = double.Parse(_a);
                double s = double.Parse(_s);
                double St = a * s;
                //Na = (ha * 10000) / St;
                Na = 10000 / St;
                Console.WriteLine("valor St getStringArbolesHectarea marco normal " + St);
            }
            else if (_tipo == "1")
            {
                double ha = double.Parse(_ha);
                double a = double.Parse(_a);
                double s = double.Parse(_s);
                double St = (Math.Sqrt(Math.Pow(a, 2) - (Math.Pow(s, 2) / 4.0))) * s;
                Console.WriteLine("getStringArbolesHectarea marco tresbolillo " + St);
                //Na = (ha * 10000) / St;
                Na = 10000 / St;
            }
            Console.WriteLine("-- fin funcion getStringArbolesHectarea -- " + _tipo);
            return String.Format(culture, "{0}", Convert.ToInt32(Na));
            //return Convert.ToInt64(Na).ToString();
        }


       public string getStringVolumenCopaHectarea(double _nha, string _vca)
        {
            Console.WriteLine("Averiguando Vha resultado de Nha =" + _nha + " X vca = " + _vca);
            double Vha = 0.0; // m3 / ha
            //double nha = double.Parse(_nha);
            double vca = GetDouble(_vca, 0.0);
            Console.WriteLine("getStringVolumenCopaHectarea, Caculamos Vha multiplicando Nha =" + _nha + " X vca = " + vca);
            Vha = (_nha * vca);
            return String.Format(culture, "{0:N2}", Vha);
        }

        public string getStringVolumenCaldoHectarea(double _Vha, string _tipo_sistema_cultivo, string densidad_hojas_id)
        {
            Console.WriteLine("calculando Vca para sistema de cultivo " + _tipo_sistema_cultivo + " " + _Vha);
            double Vca = 0.0;
            double Vha = _Vha;
            Console.WriteLine("Vha:" + Vha);
            if (_tipo_sistema_cultivo == "1")
            {
                Vca = Vha * 0.12;
                Console.WriteLine("getStringVolumenCaldoHectarea " + Vca);
            }
            else if (_tipo_sistema_cultivo == "0")
            {
                double constante = 0.10;
                if (densidad_hojas_id == "0")
                {
                    constante = 0.85;
                }
                else if (densidad_hojas_id == "2")
                {
                    constante = 0.15;
                }

                Vca = Vha * 0.10 * constante;
            }
            Console.WriteLine("-- getStringVolumenCaldoHectarea" + Vca);
            return String.Format(culture, "{0:N2}", Vca);
        }



    public string getStringCopaVCA(string _H, string _D1, string _D2)
        {
            Console.WriteLine("recibimos " + _H + " " + _D1 + " " + _D2 + " " + _D1.Length);
            //Console.WriteLine("recibimos1" + _H);
            double toReturn = 0.0;
            try
            {

                double H = 0.0;
                double D1 = 0.0;
                double D2 = 0.0;
                try
                {
                    if (_H.Length > 0 || _H != null)
                    {
                        H = double.Parse(_H);
                    }
                    if (_D1.Length > 0 || _D1 != null)
                    {
                        D1 = double.Parse(_D1);
                    }
                    if (_D2.Length > 0 || _D2 != null)
                    {
                        D2 = double.Parse(_D2);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error haciendo parsing double en getStringCopaVCA: " + ex.Message);
                }

                toReturn = (4.00 / 3.00) * Math.PI;
                toReturn = toReturn * ((H / 2.0) * (D1 / 2.0) * (D2 / 2.0));

                var x = Convert.ToDouble(4 / 3);
                Console.WriteLine("getStringCopaVCA vamos a devolver1:" + String.Format("{0:N4}", x));
                x *= Math.PI;
                Console.WriteLine("getStringCopaVCA vamos a devolver2:" + String.Format("{0:N4}", x));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("devolvemos " + "{0:N2}", toReturn);
            return String.Format(culture, "{0:N2}", toReturn); // return toReturn.ToString(specifier, culture);
        }


    */


