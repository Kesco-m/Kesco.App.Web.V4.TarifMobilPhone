using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kesco.Lib.BaseExtention;
using Kesco.Lib.BaseExtention.Enums.Controls;
using Kesco.Lib.DALC;
using Kesco.Lib.Entities.Corporate;
using Kesco.Lib.Localization;
using Kesco.Lib.Web;
using Kesco.Lib.Web.Controls.V4;
using Kesco.Lib.Web.Settings;
using CheckBox = Kesco.Lib.Web.Controls.V4.CheckBox;
using Convert = Kesco.Lib.ConvertExtention.Convert;
using Page = Kesco.Lib.Web.Controls.V4.Common.Page;
using TextBox = Kesco.Lib.Web.Controls.V4.TextBox;
using Utils = Kesco.Lib.ConvertExtention;


namespace Kesco.App.Web.TarifMobilPhone
{
    public partial class Details : Page
    {
        protected string PrintResponse = "";

        /// <summary>
        /// Менеджер ресурсов для доступа к библиотеке ресурсов Localization.dll
        /// </summary>
        public ResourceManager Resx = Resources.Resx;
        /// <summary>
        /// Словарь с параметрами строки запроса
        /// </summary>
        public Dictionary<string, string> _qsParams = new Dictionary<string, string>();

        #region Protected Var


        /// <summary>
        /// Переменная для получения значения надписи из ресурсного файла в зависимости от языка пользователя
        /// </summary>
        protected string TMD_Head1 = "";

        /// <summary>
        /// Переменная для получения значения надписи из ресурсного файла в зависимости от языка пользователя
        /// </summary>
        protected string TMD_Head2 = "";

        /// <summary>
        /// Переменная для получения значения надписи из ресурсного файла в зависимости от языка пользователя
        /// </summary>
        protected string TMD_Head3 = "";

        /// <summary>
        /// Переменная для получения значения надписи из ресурсного файла в зависимости от языка пользователя
        /// </summary>
        protected string TMD_Head4 = "";

        /// <summary>
        /// Переменная для получения значения надписи из ресурсного файла в зависимости от языка пользователя
        /// </summary>
        protected string TMD_TblColumn1 = "";

        /// <summary>
        /// Переменная для получения значения надписи из ресурсного файла в зависимости от языка пользователя
        /// </summary>
        protected string TMD_TblColumn2 = "";

        /// <summary>
        /// Переменная для получения значения надписи из ресурсного файла в зависимости от языка пользователя
        /// </summary>
        protected string TMD_TblColumn3 = "";

        /// <summary>
        /// Переменная для получения значения надписи из ресурсного файла в зависимости от языка пользователя
        /// </summary>
        protected string TMD_TblColumn4 = "";

        /// <summary>
        /// Переменная для получения значения надписи из ресурсного файла в зависимости от языка пользователя
        /// </summary>
        protected string TMD_TblColumn5 = "";

        /// <summary>
        /// Переменная для получения значения надписи из ресурсного файла в зависимости от языка пользователя
        /// </summary>
        protected string TMD_TblColumn6 = "";

        /// <summary>
        /// Переменная для получения значения надписи из ресурсного файла в зависимости от языка пользователя
        /// </summary>
        protected string TMD_TblColumn7 = "";

        /// <summary>
        /// Переменная для получения значения надписи из ресурсного файла в зависимости от языка пользователя
        /// </summary>
        protected string TMD_TblColumn8 = "";

        /// <summary>
        /// Переменная для получения значения надписи из ресурсного файла в зависимости от языка пользователя
        /// </summary>
        protected string TMD_TblColumn9 = "";

        /// <summary>
        /// Переменная для получения значения надписи из ресурсного файла в зависимости от языка пользователя
        /// </summary>
        protected string TMD_TblFooter = "";

        /// <summary>
        /// Переменная для получения значения надписи из ресурсного файла в зависимости от языка пользователя
        /// </summary>
        protected string TMD_Title = "";

        /// <summary>
        /// Переменная для получения значения надписи из ресурсного файла в зависимости от языка пользователя
        /// </summary>
        protected string TM_FDogovor = "";

        /// <summary>
        /// Переменная для получения значения надписи из ресурсного файла в зависимости от языка пользователя
        /// </summary>
        protected string TM_FEmployee = "";

        /// <summary>
        /// Переменная для получения значения надписи из ресурсного файла в зависимости от языка пользователя
        /// </summary>
        protected string TM_FPhone = "";

        /// <summary>
        /// Переменная для получения значения надписи из ресурсного файла в зависимости от языка пользователя
        /// </summary>
        protected string TM_FYear = "";

        /// <summary>
        /// Переменная для получения значения надписи из ресурсного файла в зависимости от языка пользователя
        /// </summary>
        protected string TM_TblColumn4_0 = "";

        /// <summary>
        /// Переменная для получения значения надписи из ресурсного файла в зависимости от языка пользователя
        /// </summary>
        protected string TM_TblColumn4_1 = "";

        /// <summary>
        /// Строка для передачи на клиента в статус окна соответствующего значения из ресурсного файла
        /// </summary>
        protected string LTotalFound = "";

        /// <summary>
        /// Строка для передачи на клиента в статус окна соответствующего значения из ресурсного файла
        /// </summary>
        protected string Title_Help = "";


        /// <summary>
        /// Строка для передачи на клиента в легенду с описанием цветов соответствующего значения из ресурсного файла
        /// </summary>
        protected string LegendTitle = "";

        /// <summary>
        /// Переменная для получения значения надписи из ресурсного файла в зависимости от языка пользователя
        /// </summary>
        protected string TitleGray = "";

        /// <summary>
        /// Переменная для получения значения надписи из ресурсного файла в зависимости от языка пользователя
        /// </summary>
        protected string TitleGreen = "";

        /// <summary>
        /// Переменная для получения значения надписи из ресурсного файла в зависимости от языка пользователя
        /// </summary>
        protected string TitleOrange = "";

        /// <summary>
        /// Переменная для получения значения надписи из ресурсного файла в зависимости от языка пользователя
        /// </summary>
        protected string TitleWhite = "";

        /// <summary>
        /// Переменная для получения значения надписи из ресурсного файла в зависимости от языка пользователя
        /// </summary>
        protected string TM_OutSimData = "";


        /// <summary>
        ///Переменная для получения значения надписи из ресурсного файла в зависимости от языка пользователя
        /// </summary>
        private string TM_Issued = "";

        /// <summary>
        ///Переменная для получения значения надписи из ресурсного файла в зависимости от языка пользователя
        /// </summary>
        private string TM_Withdrawn = "";

        #endregion


        /// <summary>
        /// Параметр для приведения переданного значения из строки запроса - НДС на общую сумму
        /// </summary>
        private int _ndsAllSumm;

        /// <summary>
        /// Параметр для приведения переданного значения из строки запроса - НДС в тарификации
        /// </summary>
        private int _ndsIn;

        /// <summary>
        /// Параметр для приведения переданного значения из строки запроса - Ставка НДС
        /// </summary>
        private decimal _ndsStavka;

        /// <summary>
        /// Параметр для приведения переданного значения из строки запроса - Точность округления
        /// </summary>
        private int _scale;

        private bool isPrintPage = false;

        /// <summary>
        /// Константа цвета фона, в зависимости от условий договора
        /// </summary>
        private readonly string _colorGray = "lightgray";
        /// <summary>
        /// Константа цвета фона, в зависимости от условий договора
        /// </summary>
        private readonly string _colorWhite = "white";
        /// <summary>
        /// Константа цвета фона, в зависимости от условий договора
        /// </summary>
        private readonly string _colorOrange = "darkorange";

        /// <summary>
        /// Получение значений строк из ресурсных файлов
        /// </summary>
        protected override void ResxResourceSet()
        {
            LegendTitle = Resx.GetString("TM_LegendTitle");
            TitleGreen = Resx.GetString("TM_TitleGreen");
            TitleGray = Resx.GetString("TM_TitleGray2");
            TitleWhite = Resx.GetString("TM_TitleWhite");
            TitleOrange = Resx.GetString("TM_TitleOrange");

            TMD_TblColumn1 = Resx.GetString("TMD_TblColumn1");
            TMD_TblColumn2 = Resx.GetString("TMD_TblColumn2");
            TMD_TblColumn3 = Resx.GetString("TMD_TblColumn3");
            TMD_TblColumn4 = Resx.GetString("TMD_TblColumn4");
            TMD_TblColumn5 = Resx.GetString("TMD_TblColumn5");
            TMD_TblColumn6 = Resx.GetString("TMD_TblColumn6");
            TMD_TblColumn7 = Resx.GetString("TMD_TblColumn7");
            TMD_TblColumn8 = Resx.GetString("TMD_TblColumn8");
            TMD_TblColumn9 = Resx.GetString("TMD_TblColumn9");
            TMD_TblFooter = Resx.GetString("TMD_TblFooter");
            TMD_Head1 = Resx.GetString("TMD_Head1");
            TMD_Head2 = Resx.GetString("TMD_Head2");
            TMD_Title = Resx.GetString("TMD_Title");
            TMD_Head3 = Resx.GetString("TMD_Head3");
            TMD_Head4 = Resx.GetString("TMD_Head4");

            TM_FDogovor = Resx.GetString("TM_FDogovor");
            TM_FEmployee = Resx.GetString("TM_FEmployee");
            TM_FYear = Resx.GetString("TM_FYear");
            TM_TblColumn4_0 = Resx.GetString("TM_TblColumn4_0");
            TM_TblColumn4_1 = Resx.GetString("TM_TblColumn4_1");
            TM_FPhone = Resx.GetString("TM_FPhone");
            TM_OutSimData = Resx.GetString("TM_OutSimData");
            TM_Issued = Resx.GetString("TM_Issued");
            TM_Withdrawn = Resx.GetString("TM_Withdrawn");


            Title_Help = Resx.GetString("Title_Help");

            LTotalFound = Resx.GetString("lTotalFound");
        }

        protected override string HelpUrl
        {
            get;
            set;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!V4IsPostBack)
            {
                ResxResourceSet();

                if (Request.QueryString["view"] == "print")
                {
                    isPrintPage = true;
                    Print();
                    ShowMessage("Пока не сделано!","Сообщение",MessageStatus.Information);
                    return;
                }
                
                GetQSParams();

                SetLocalVarFromQS();

                RenderTitle();
                FillDataGrid();

                HelpUrl = "hlp/help.htm?page=hlpDetails";
            }
        }

        /// <summary>
        /// Инициализирует объект <see cref="T:System.Web.UI.HtmlTextWriter"/> и вызывает дочерние элементы управления страницы <see cref="T:System.Web.UI.Page"/> для отображения.
        /// </summary>
        /// <param name="w"><see cref="T:System.Web.UI.HtmlTextWriter"/>, получающий содержимое страницы.</param>
        protected override void Render(HtmlTextWriter w)
        {
            if (!String.IsNullOrEmpty(PrintResponse))
            {
                w.Write(PrintResponse);
                return;
            }

            base.Render(w);
        }


        /// <summary>
        /// Функция, формирующая словь с параметрами в зависимости от установленнного фильтра
        /// </summary>
        /// <returns>Словарь с параметрами</returns>
        private Dictionary<string, object> GetSQLParams()
        {
            var sqlParams = new Dictionary<string, object>();

            sqlParams.Add("@OpenMonth", new object[] { _qsParams["OpenMonth"], DBManager.ParameterTypes.Int32 });
            sqlParams.Add("@Год", new object[] { _qsParams["Year"], DBManager.ParameterTypes.Int32 });
            sqlParams.Add("@Месяц", new object[] { _qsParams["Month"], DBManager.ParameterTypes.Int32 });

            sqlParams.Add("@КодДоговора", new object[] { _qsParams["Dogovor"], DBManager.ParameterTypes.Int32 });

            if (_qsParams["bln"].Equals("0"))
                sqlParams.Add("@Договор", new object[] { "", DBManager.ParameterTypes.String });
            else
                sqlParams.Add("@Договор", new object[] { _qsParams["DogovorT"], DBManager.ParameterTypes.String });

            sqlParams.Add("@Ключ", new object[] { _qsParams["UserT"], DBManager.ParameterTypes.String });
            sqlParams.Add("@Абонент", new object[] { _qsParams["Phone"] == "- абонент неизвестен -" ? "" : _qsParams["Phone"], DBManager.ParameterTypes.String });

            return sqlParams;
        }

        /// <summary>
        /// Метод заполнения таблицы данными
        /// </summary>
        private void FillDataGrid()
        {
            // для последующей отрисовки времени выполнения
            var x = DateTime.Now.ToLongTimeString();

            Dictionary<string, object> sqlParams = GetSQLParams();
            gridData.SetDataSource(DBManager.GetData(SQL_Queries.SQL_ТарификацияСотовых_ДляКонтрола,
                Config.DS_accounting_phone,
                CommandType.Text,
                sqlParams));

            var x0 = DateTime.Now.ToLongTimeString();

            // Установки видимости колонки
            gridData.Settings.SetColumnDisplayVisible("Абонент", false);
            // Установка алиасов
            gridData.Settings.SetColumnHeaderAlias("НачалоРазговора", "Дата и время");
            gridData.Settings.SetColumnHeaderAlias("Описание", "Описание услуги");
            gridData.Settings.SetColumnHeaderAlias("Секунд", "Длительность");
            gridData.Settings.SetColumnHeaderAlias("СуммаСотрудника", "Сумма, оплачиваемая сотрудниками");
            // Установка формата данных
            gridData.Settings.SetColumnFormat("Сумма", "N2");
            gridData.Settings.SetColumnFormat("СуммаСотрудника", "N2");
            gridData.Settings.SetColumnFormat("НачалоРазговора", "dd.MM.yy HH:mm:ss");
            gridData.Settings.SetColumnFormat("Килобайт", "N");
            // Преобразует секунды в строку формата HH:MM:SS
            gridData.Settings.SetColumnIsTimeSecond("Секунд");
            // Значения итоговой строки
            gridData.Settings.SetColumnSumValuesText("Описание", "Итого");
            gridData.Settings.SetColumnIsSumValues("Секунд");
            gridData.Settings.SetColumnIsSumValues("Килобайт");
            gridData.Settings.SetColumnIsSumValues("Сумма");
            gridData.Settings.SetColumnIsSumValues("СуммаСотрудника");

            gridData.Settings.SetColumnBackGroundColor("Сумма", _qsParams["Color"]);
            gridData.Settings.SetColumnBackGroundColor("СуммаСотрудника", _qsParams["Color"]);
            // фон колонок
            var tdTitle = GetTitleByColor();
            gridData.Settings.SetColumnTitle("Сумма", tdTitle);
            gridData.Settings.SetColumnTitle("СуммаСотрудника", tdTitle);

            gridData.RefreshGridData();

            var x1 = DateTime.Now.ToLongTimeString();
            if (Request.QueryString["showTime"]!=null) 
                JS.Write("$(\"#TimeSpan\").html(\"Начало: {0} -> Получили данные: {1} -> Отрисовка: {2}\");", x, x0, x1);
        }

        /// <summary>
        /// Процедура, заполняющая словарь параметрами, переданными со строкой запросов
        /// </summary>
        private void GetQSParams()
        {
            _qsParams.Add("OpenMonth", Request.QueryString["OpenMonth"]);
            _qsParams.Add("Year", Request.QueryString["Year"]);
            _qsParams.Add("Month", Request.QueryString["Month"]);
            _qsParams.Add("Dogovor", Request.QueryString["Dogovor"]);
            _qsParams.Add("bln", Request.QueryString["bln"]);
            _qsParams.Add("DogovorT", Request.QueryString["DogovorT"]);
            _qsParams.Add("User", Request.QueryString["User"]);
            _qsParams.Add("UserT", Request.QueryString["UserT"]);
            _qsParams.Add("Phone", Request.QueryString["Phone"]);

            _qsParams.Add("NDSAllSumm", Request.QueryString["NDSAllSumm"]);
            _qsParams.Add("NDSIn", Request.QueryString["NDSIn"]);
            _qsParams.Add("NDSStavka", Request.QueryString["NDSStavka"]);
            _qsParams.Add("Scale", Request.QueryString["Scale"]);

            _qsParams.Add("Color", Request.QueryString["Color"]);
            _qsParams.Add("Title", Request.QueryString["Title"]);
        }

        private void SetLocalVarFromQS()
        {
            _ndsIn = int.Parse(_qsParams["NDSIn"]);
            _ndsAllSumm = int.Parse(_qsParams["NDSAllSumm"]);
            _ndsStavka = Convert.Str2Decimal(_qsParams["NDSStavka"]);
            _scale = int.Parse(_qsParams["Scale"]);
        }

        /// <summary>
        /// Процедура вывода статического заголовка таблицы
        /// </summary>
        private StringWriter RenderTitle()
        {
            var w = new StringWriter();
            string _qsUser = _qsParams["User"];
            string _qsUserT = _qsParams["UserT"];
            string _qsDogovor = _qsParams["Dogovor"];
            string _qsDogovorT = _qsParams["DogovorT"];
            string _qsPhone = _qsParams["Phone"];
            string _qsYear = _qsParams["Year"];
            string _qsMonth = _qsParams["Month"];
            string _qsbln = _qsParams["bln"];
            string _qsOpenMonth = _qsParams["OpenMonth"];
            string _space = "&nbsp;&nbsp;&nbsp;";

            if (_qsbln.Length > 0 && _qsbln.Equals("1"))
            {
                w.Write("{0};", HttpUtility.HtmlEncode(TMD_Head4));
            }
            else
            {
                if (_qsUser.Length > 0)
                {
                    var user = new Employee(_qsUser);
                    if (!user.Unavailable)
                    {
                        w.Write("{0} - ", HttpUtility.HtmlEncode(TM_FEmployee));

                        if (isPrintPage)
                            w.Write(_qsUserT);
                        else
                        {
                            string url = Config.user_form + "?id=" + user.Id;
                            RenderLinkEmployee(w, "userLink" + user.Id, user.Id, _qsUserT, NtfStatus.Empty);
                        }
                    }
                    else
                    {
                        w.Write(_qsUserT); 
                    }
                    w.Write(";");
                }
                else 
                    w.Write(_qsUserT);

                if (_qsPhone.Length > 0)
                {
                    w.Write(_space);
                    w.Write("{0} - {1};", HttpUtility.HtmlEncode(TMD_Head1), _qsPhone);
                    RenderSim(w, _qsUser, _qsPhone, _qsYear, _qsMonth, _qsOpenMonth);

                }

                if (_qsDogovorT.Length > 0)
                {
                    w.Write(_space);
                    w.Write("{0} - {1};", HttpUtility.HtmlEncode(TM_FDogovor), _qsDogovorT);
                }
            }


            if (_qsOpenMonth.Length > 0 && _qsOpenMonth.Equals("1"))
            {
                w.Write(_space);
                w.Write("{0};", HttpUtility.HtmlEncode(TMD_Head2));
            }
            else
            {
                string _qsMonthName = "";
                if (_qsMonth.Length > 0 && !_qsMonth.Equals("0"))
                    _qsMonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(int.Parse(_qsMonth)) +
                                   "&nbsp;";


                string _qsYearName = "";
                if (_qsYear.Length > 0 && !_qsYear.Equals("0"))
                    _qsYearName = _qsYear + string.Format("&nbsp;{0}", HttpUtility.HtmlEncode(TM_FYear).ToLower());


                w.Write(_space);
                w.Write("{0} - {1}{2}", HttpUtility.HtmlEncode(TMD_Head3), _qsMonthName, _qsYearName);
            }


            if (isPrintPage) return w;

            w.Write(
             "<img src=\"/styles/print.gif\" border=\"0\" title=\"Печать\" style=\"cursor:pointer; margin-right:10px; margin-left:30px;\" onclick=\"PrintData();\">");

            w.Write(
                @"&nbsp;<a style=""margin-left:100px;"" href=""javascript:void(0);"" onclick=""v4_openHelp('{1}');"" class=""btn""><img src=""/styles/Help.gif"" border=""0"" title=""{0}"" ></a>", Title_Help, IDPage);


            JS.Write("var objT = document.getElementById('divTitle'); if (objT) objT.innerHTML='{0}';",
                     HttpUtility.JavaScriptStringEncode(w.ToString()));

            return w;
        }

        private void RenderSim(TextWriter w, string qsUser, string qsPhone, string qsYear, string qsMonth, string qsOpenMonth)
        {
            if (qsUser.Length == 0 || qsPhone.Length == 0) return;


            Dictionary<string, object> sqlParams = new Dictionary<string, object>();
            DateTime startDate = DateTime.MinValue;
            DateTime endDate = DateTime.MinValue;

            if (qsMonth.Length > 0 && !qsMonth.Equals("0"))
            {
                startDate = new DateTime(int.Parse(qsYear), int.Parse(qsMonth), 1);
                endDate = new DateTime(int.Parse(qsYear), int.Parse(qsMonth),
                    DateTime.DaysInMonth(int.Parse(qsYear), int.Parse(qsMonth)));
            }
            else
            {
                startDate = DateTime.Today;
                endDate = startDate;
            }

            if (qsOpenMonth.Length == 0) qsOpenMonth = "0";

            sqlParams.Add("@StartDate", startDate);
            sqlParams.Add("@EndDate", endDate);
            sqlParams.Add("@CurrentMonth", int.Parse(qsOpenMonth));
            sqlParams.Add("@КодСотрудника", int.Parse(qsUser));
            sqlParams.Add("@НомерТелефона", qsPhone);

            DataTable dt = DBManager.GetData(SQL_Queries.SQL_ПолучениеИнформацииПоSIM, Config.DS_user,
                                            CommandType.Text, sqlParams);
            DateTime dateTo = DateTime.MinValue;
            DateTime dateFrom = DateTime.MinValue;
            bool render = true;

            StringBuilder sb = new StringBuilder();
            if (dt.Rows.Count == 0)
            {
                //sb.AppendFormat("<font style='color:red;'>{0}</font>", TM_OutSimData);
            }
            else
            {
                sb.AppendFormat(@"<img src='/styles/sim.gif' border='0'>");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dateFrom = dt.Rows[i]["От"].Equals(System.DBNull.Value) ? dateTo : (DateTime)dt.Rows[i]["От"];
                    dateTo = dt.Rows[i]["До"].Equals(System.DBNull.Value) ? dateTo : (DateTime)dt.Rows[i]["До"];

                    //if (!dateFrom.Equals(DateTime.MinValue) && dateFrom.Date >= startDate)
                    sb.AppendFormat("{1}: {0};", ((DateTime)dt.Rows[i]["От"]).ToString("dd.MM.yyyy"), TM_Issued);

                    if (!dateTo.Equals(DateTime.MinValue) && dateTo.Date <= endDate)
                        sb.AppendFormat("{1}: {0};", dateTo.ToString("dd.MM.yyyy"), TM_Withdrawn);

                    sb.AppendFormat("{0}",
                        dt.Rows[i]["Примечания"].ToString().Length > 0
                            ? " " + dt.Rows[i]["Примечания"] + ";"
                            : "");

                    //render = dt.Rows[i]["Примечания"].ToString().Length > 0;
                    dateTo = DateTime.MinValue;
                }
            }
            w.Write(sb.ToString());

        }

        private string GetTitleByColor()
        {
            string[] colorStyle = _qsParams["Color"].Split(':');
            if (colorStyle.Length < 2) return "";
            string color = colorStyle[1].Replace(";", "").Trim();
            string title = "{0}";

            if (color.Equals(_colorWhite))
                title = string.Format(title, TitleWhite);
            else if (color.Equals(_colorGray))
                title = string.Format(title, TitleGray);
            else
                title = string.Format(title, TitleOrange);
            return title;
        }

        private void Print()
        {
            string idpage = Request.QueryString["idpage"];
            if (String.IsNullOrEmpty(idpage))
            {
                ShowMessage("Ошибка получения идентификатора страницы", "Ошибка печати", MessageStatus.Error);
                return;
            }
            var p = Application[idpage] as Page;
            if (p == null)
            {
                ShowMessage("Ошибка получения объекта страницы", "Ошибка печати", MessageStatus.Error);
                return;
            }

            _qsParams = ((Details)p)._qsParams;
            StringWriter w = new StringWriter();

            w.Write("<html><head><title>{0}</title><style>table {{border-collapse: collapse;}} table, td, th {{border: 1px solid black;}} .tdR{{text-align:right}} .tdC{{text-align:center}}</style></head><body onload=\"window.print()\">", Resx.GetString("lPrint"));
            w.Write("<h3>Детализированный отчёт по телефонным звонкам</h3>");
            w.Write("<h4>Дата формирования: {0}</h4>",
                DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " MSK");

            w.Write(RenderTitle().ToString());
            w.Write("<br/><br/>");

            Kesco.Lib.Web.Controls.V4.Grid.Grid gridDataPrint = ((Details)p).gridData;
            gridDataPrint.Settings.IsPrintVersion = true;
            gridDataPrint.RenderGridData(w);
            gridDataPrint.Settings.IsPrintVersion = false;

            w.Write("</body></html>");
            PrintResponse = w.ToString();

        }
    }
}