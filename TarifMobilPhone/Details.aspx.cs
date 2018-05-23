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
using System.Web.UI.WebControls;
using Kesco.Lib.BaseExtention;
using Kesco.Lib.BaseExtention.Enums.Controls;
using Kesco.Lib.DALC;
using Kesco.Lib.Entities.Corporate;
using Kesco.Lib.Localization;
using Kesco.Lib.Web;
using Kesco.Lib.Web.Controls.V4;
using Kesco.Lib.Web.Controls.V4.Common;
using Kesco.Lib.Web.Settings;
using CheckBox = Kesco.Lib.Web.Controls.V4.CheckBox;
using Convert = Kesco.Lib.ConvertExtention.Convert;
using TextBox = Kesco.Lib.Web.Controls.V4.TextBox;
using Utils = Kesco.Lib.ConvertExtention;

namespace Kesco.App.Web.TarifMobilPhone
{
    /// <summary>
    /// Класс формы details.aspx - Детализированный отчёт по телефонным звонкам
    /// </summary>
    public partial class Details : Page
    {
        #region Define dynamic control

        /// <summary>
        /// Фильтр:Описание услуги тарификации
        /// </summary>
        private ComboBox cbDescription;

        /// <summary>
        /// Фильтр:Протарифицированная услуга
        /// </summary>
        private ComboBox cbService;

        /// <summary>
        /// Фильтр:Тип вызова
        /// </summary>
        private ComboBox cbType;

        /// <summary>
        /// Фильтр:Роуминг
        /// </summary>
        private ComboBox cbRouming;

        /// <summary>
        /// Группировка по направлению тарификации
        /// </summary>
        private CheckBox chDescription;

        /// <summary>
        /// Группировка по номеру телефона
        /// </summary>
        private CheckBox chPhone;

        /// <summary>
        /// Группировка по услуге
        /// </summary>
        private CheckBox chService;

        /// <summary>
        /// Группировка по типу вызова
        /// </summary>
        private CheckBox chType;

        /// <summary>
        /// Группировка по роумингу
        /// </summary>
        private CheckBox chRouming;
        
        /// <summary>
        /// Фильтр:Телефонный номер
        /// </summary>
        private TextBox txtPhone;

        #endregion

        /// <summary>
        /// Менеджер ресурсов для доступа к библиотеке ресурсов Localization.dll
        /// </summary>
        public ResourceManager Resx = Resources.Resx;

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

        #region Private Var
       
        /// <summary>
        /// Словарь с параметрами строки запроса
        /// </summary>
        private readonly Dictionary<string, string> _qsParams = new Dictionary<string, string>();

        /// <summary>
        /// Словарь с типизированными колонками
        /// </summary>
        private Dictionary<string, Type> _columnList;

        /// <summary>
        /// Используется для работы со списком данных
        /// </summary>
        private DataView _dvData;

        /// <summary>
        /// Словарь колонками, участвующими в группировке
        /// </summary>
        private Dictionary<string, bool> _groupByList;


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

        /// <summary>
        /// Очередь сортировки
        /// </summary>
        private SpecialQueue<string> _sqSort;

        /// <summary>
        /// Словарь с колонками сумм
        /// </summary>
        private Dictionary<string, decimal> _sumList;

        #endregion

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

        #region Override

        /// <summary>
        /// Обработчик события загрузки страницы
        /// </summary>
        /// <param name="sender">Страница</param>
        /// <param name="e">Параметры</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!V4IsPostBack)
            {
                FillColumnList();
                FillGroupByList();
                FillSumList();


                // SetDefaultPagerSettings();

                GetQSParams();

                _ndsIn = int.Parse(_qsParams["NDSIn"]);
                _ndsAllSumm = int.Parse(_qsParams["NDSAllSumm"]);
                _ndsStavka = Convert.Str2Decimal(_qsParams["NDSStavka"]);
                _scale = int.Parse(_qsParams["Scale"]);

                ResxResourceSet();

                _dvData = GetData(null, "");
                _sqSort = new SpecialQueue<string>();

                RenderTitle();
                RenderTable(_dvData);

                HelpUrl = "hlp/help.htm?page=hlpDetails";
                // Pager.CurrentPageChanged += new EventHandler(Pager_CurrentPageChanged);
                // Pager.RowsPerPageChanged += new EventHandler(Pager_RowsPerPageChanged);
            }
        }

        /// <summary>
        /// Обработчик команд V4, которые посылаются с клиента на сервер:
        /// <list type="bullet">
        /// <item>
        /// <description>Sort - асинхронная команда, сортировки по указанной колонке</description>
        /// </item>
        /// <item>
        /// <description>checkbox - асинхронная команда, изменения значения в контролах типа checkbox</description>
        /// </item>
        /// <item>
        /// <description>combobox - асинхронная команда, изменения значения в контролах типа combobox</description>
        /// </item>
        /// <item>
        /// <description>textbox - асинхронная команда, изменения значения в контролах типа textbox</description>
        /// </item>
        /// </list>
        /// </summary>
        /// <param name="cmd">Название команды</param>
        /// <param name="param">Коллекция параметров</param>
        protected override void ProcessCommand(string cmd, NameValueCollection param)
        {
            switch (cmd)
            {
                case "Sort":
                    SortDataByColumn(param["Column"]);
                    RestoreCursor();
                    break;
                case "checkbox":
                    CheckBoxChanged(param["Column"], param["Value"]);
                    break;
                case "combobox":
                case "textbox":
                    RefreshTableBody();
                    RestoreCursor();
                    break;
                default:
                    base.ProcessCommand(cmd, param);
                    break;
            }
        }
        
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
            get; set;
        }

        #endregion

        #region Fill Dictionary

        /// <summary>
        /// Процедура заполнения списка колонок
        /// </summary>
        private void FillColumnList()
        {
            _columnList = new Dictionary<string, Type>();
            _columnList.Add("НачалоРазговора", typeof (string));
            _columnList.Add("Роуминг", typeof(string));
            _columnList.Add("Телефон", typeof (string));
            _columnList.Add("Тип", typeof (string));
            _columnList.Add("Услуга", typeof (string));
            _columnList.Add("Описание", typeof(string));
            _columnList.Add("Секунд", typeof (decimal));
            _columnList.Add("Килобайт", typeof (decimal));
            _columnList.Add("СуммаСотрудника", typeof (decimal));
            _columnList.Add("Сумма", typeof (decimal));
        }

        /// <summary>
        /// Процедура заполнения списка колонок, участвующих в группировке
        /// </summary>
        private void FillGroupByList()
        {
            _groupByList = new Dictionary<string, bool>();
            _groupByList.Add("Телефон", false);
            _groupByList.Add("Тип", false);
            _groupByList.Add("Услуга", false);
            _groupByList.Add("Описание", false);
        }

        /// <summary>
        /// Процедура заполнения списка колонок, участвующих в суммировании
        /// </summary>
        private void FillSumList()
        {
            _sumList = new Dictionary<string, decimal>();
            _sumList.Add("Секунд", 0);
            _sumList.Add("Килобайт", 0);
            _sumList.Add("СуммаСотрудника", 0);
            _sumList.Add("Сумма", 0);
        }

        #endregion

        #region Load Data

        /// <summary>
        /// Функция, формирующая словь с параметрами в зависимости от установленнного фильтра
        /// </summary>
        /// <returns>Словарь с параметрами</returns>
        private Dictionary<string, object> GetSQLParams()
        {
            var sqlParams = new Dictionary<string, object>();

            sqlParams.Add("@OpenMonth", new object[] {_qsParams["OpenMonth"], DBManager.ParameterTypes.Int32});
            sqlParams.Add("@Год", new object[] {_qsParams["Year"], DBManager.ParameterTypes.Int32});
            sqlParams.Add("@Месяц", new object[] {_qsParams["Month"], DBManager.ParameterTypes.Int32});

            sqlParams.Add("@КодДоговора", new object[] {_qsParams["Dogovor"], DBManager.ParameterTypes.Int32});

            if (_qsParams["bln"].Equals("0"))
                sqlParams.Add("@Договор", new object[] {"", DBManager.ParameterTypes.String});
            else
                sqlParams.Add("@Договор", new object[] {_qsParams["DogovorT"], DBManager.ParameterTypes.String});

            sqlParams.Add("@КодСотрудника", new object[] {_qsParams["User"], DBManager.ParameterTypes.Int32});
            sqlParams.Add("@Абонент", new object[] {_qsParams["Phone"], DBManager.ParameterTypes.String});

            return sqlParams;
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
            _qsParams.Add("Phone", Request.QueryString["Phone"]);

            _qsParams.Add("NDSAllSumm", Request.QueryString["NDSAllSumm"]);
            _qsParams.Add("NDSIn", Request.QueryString["NDSIn"]);
            _qsParams.Add("NDSStavka", Request.QueryString["NDSStavka"]);
            _qsParams.Add("Scale", Request.QueryString["Scale"]);

            _qsParams.Add("Color", Request.QueryString["Color"]);
            _qsParams.Add("Title", Request.QueryString["Title"]);
        }

        /// <summary>
        /// Процедура получения данных
        /// </summary>
        /// <param name="localParams">Параметры фильтрации, установленные на форме</param>
        /// <param name="sort">Порядок сортировки</param>
        /// <returns>Источник данных</returns>
        private DataView GetData(StringCollection localParams, string sort)
        {
            //string _pageNum = Pager.CurrentPageNumber.ToString();
            // string _itemsPerPage = Pager.RowsPerPage.ToString();
            // string _pageCount = Pager.MaxPageNumber.ToString();
            string _pageNum = "1";
            string _itemsPerPage = "100000";
            string _pageCount = "1"; //"Pager.MaxPageNumber.ToString()";
            string _sRez = "";

            Dictionary<string, object> sqlParams = GetSQLParams();
            DataTable dt = DBManager.GetData(SQL_Queries.SQL_ТарификацияСотовых,
                                             Config.DS_accounting_phone,
                                             CommandType.Text,
                                             sqlParams,
                                             localParams,
                                             sort,
                                             "НачалоРазговора DESC",
                                             _columnList,
                                             _groupByList,
                                             _sumList,
                                             ref _pageNum,
                                             ref _itemsPerPage,
                                             ref _pageCount,
                                             out _sRez
                );

            //Pager.CurrentPageNumber = int.Parse(_pageNum);
            //Pager.MaxPageNumber = int.Parse(_pageCount);
            //Pager.RowsPerPage = int.Parse(_itemsPerPage);

            //if (!(int.Parse(_sRez) <= int.Parse(_itemsPerPage)))
            //    Pager.SetDisabled(false);

            JS.Write("window.status = '{0}';", HttpUtility.JavaScriptStringEncode(string.Format(LTotalFound, _sRez)));

            var dv = new DataView(dt);
            return dv;
        }

        #endregion

        #region Render

        /// <summary>
        /// Процедура вывода статического заголовка таблицы
        /// </summary>
        private void RenderTitle()
        {
            var w = new StringWriter();
            string _qsUser = _qsParams["User"];
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

                    w.Write("{0} - ", HttpUtility.HtmlEncode(TM_FEmployee));
                    string url = Config.user_form + "?id=" + user.Id;
                    RenderLinkEmployee(w, "userLink" + user.Id, user, NtfStatus.Empty);
                    w.Write(";");
                }

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
            w.Write(
                @"&nbsp;<a style=""margin-left:100px;"" href=""javascript:void(0);"" onclick=""v4_openHelp('{1}');"" class=""btn""><img src=""/styles/Help.gif"" border=""0"" title=""{0}"" ></a>", Title_Help, IDPage);
            JS.Write("var objT = document.getElementById('divTitle'); if (objT) objT.innerHTML='{0}';",
                     HttpUtility.JavaScriptStringEncode(w.ToString()));
        }

        /// <summary>
        /// Процедура вывода данных
        /// </summary>
        /// <param name="dv">Источник данных</param>
        private void RenderTable(DataView dv)
        {
            var w = new StringWriter();


            w.Write(
                "<table id='tblMain' style='empty-cells: show; BORDER-COLLAPSE:collapse;margin-right:15px' boder='1'>");

            w.Write("<thead>");
            RenderTableHeader(w, dv);
            w.Write("</thead>");

            decimal sum = 0M;
            decimal sumE = 0M;

            w.Write("<tbody id='tblBody' >");
            RenderTableBody(w, dv, ref sum, ref sumE);
            w.Write("</tbody>");

            w.Write("<tfoot id='tblFoot'>");
            RenderTableFooter(w, dv, sum, sumE);
            w.Write("</tfoot>");

            w.Write("</table>");

            JS.Write("var objM = document.getElementById('divMain'); if (objM) objM.innerHTML='{0}';",
                     HttpUtility.JavaScriptStringEncode(w.ToString()));
        }

        /// <summary>
        /// Процедура вывода заголовка таблицы с контролами фильтрации и группировки
        /// </summary>
        /// <param name="w">Поток вывода</param>
        /// <param name="dv">Источник данных</param>
        private void RenderTableHeader(StringWriter w, DataView dv)
        {
            #region Названия колонок

            w.Write("<tr class='gridHeader'>");

            //-------------------------------------------------------------------------------------
            w.Write(@"<td>");
            w.Write(RenderSortableColumnLink("НачалоРазговора", TMD_TblColumn1));
            w.Write("</td>");

            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            w.Write(@"<td noWrap valign='middle'>");
            chRouming = CreateCheckBox("chRouming", RenderSortableColumnLink("Роуминг", "Роуминг"),
                                       Event_chRouming_Changed);
            chRouming.RenderControl(w);
            w.Write("</td>");

            //-------------------------------------------------------------------------------------
            w.Write(@"<td noWrap valign='middle'>");
            chPhone = CreateCheckBox("chPhone", RenderSortableColumnLink("Телефон", TM_FPhone),
                                        Event_chPhone_Changed);
            chPhone.RenderControl(w);
            w.Write("</td>");

            //-------------------------------------------------------------------------------------
            w.Write(@"<td valign='middle'>");
            chType = CreateCheckBox("chType", RenderSortableColumnLink("Тип", TMD_TblColumn2),
                                       Event_chType_Changed);
            chType.RenderControl(w);
            w.Write("</td>");
            
            //-------------------------------------------------------------------------------------
            w.Write(@"<td valign='middle'>");
            chService = CreateCheckBox("chService",
                                          RenderSortableColumnLink("Услуга", TMD_TblColumn5),
                                          Event_chService_Changed);
            chService.RenderControl(w);
            w.Write("</td>");


            //-------------------------------------------------------------------------------------
            w.Write(@"<td noWrap valign='middle'>");
            chDescription = CreateCheckBox("chDescription",
                                            RenderSortableColumnLink("Описание", "Описание услуги"),
                                            Event_chDescription_Changed);
            chDescription.RenderControl(w);
            w.Write("</td>");


            //-------------------------------------------------------------------------------------
            w.Write(@"<td  valign='middle'>{0}</td>",
                    RenderSortableColumnLink("Секунд", TMD_TblColumn6));
            //-------------------------------------------------------------------------------------
            w.Write(@"<td  valign='middle'>{0}</td>",
                    RenderSortableColumnLink("Килобайт", TMD_TblColumn7));
            //-------------------------------------------------------------------------------------
            w.Write(@"<td  valign='middle'>{0}</td>",
                    RenderSortableColumnLink("СуммаСотрудника",
                                          string.Format("{0}<br>{1}", TM_TblColumn4_0,
                                                        TM_TblColumn4_1)));
            //-------------------------------------------------------------------------------------
            w.Write(@"<td  valign='middle'>{0}</td>",
                    RenderSortableColumnLink("Сумма", TMD_TblColumn9));


            w.Write("</tr>");

            #endregion

            #region Контролы фильтрации

            w.Write(@"<tr class='gridHeader'>");

            //-------------------------------------------------------------------------------------
         
            w.Write(@"<td>");
            //cbTime = CreateComboBox("cbTime", 18, 120, cbTime_Changed);
            //cbTime.RenderControl(w);
            w.Write("</td>");

            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            w.Write(@"<td >");
            cbRouming = CreateComboBox("cbRouming", 18, 50, Event_cbRouming_Changed);
            FillComboBox(cbRouming, dv, "Роуминг");
            cbRouming.RenderControl(w);
            w.Write("</td>");

            //-------------------------------------------------------------------------------------
         
            w.Write(@"<td>");
            txtPhone = CreateTextBox("txtPhone", 16, 80, Event_txtPhone_Changed);
            txtPhone.RenderControl(w);
            w.Write("</td>");

            //-------------------------------------------------------------------------------------
         
            w.Write(@"<td >");
            cbType = CreateComboBox("cbType", 18, 50, Event_cbType_Changed);
            FillComboBox(cbType, dv, "Тип");
            cbType.RenderControl(w);
            w.Write("</td>");
            
            
            //-------------------------------------------------------------------------------------
         
            w.Write(@"<td>");
            cbService = CreateComboBox("cbService", 18, 120, Event_cbService_Changed);
            FillComboBox(cbService, dv, "Услуга");
            cbService.RenderControl(w);
            w.Write("</td>");

            //-------------------------------------------------------------------------------------

            w.Write(@"<td >");
            cbDescription = CreateComboBox("cbDescription", 18, 250, Event_cbDescription_Changed);
            FillComboBox(cbDescription, dv, "Описание");
            cbDescription.RenderControl(w);
            w.Write("</td>");

            //-------------------------------------------------------------------------------------
         
            w.Write(@"<td></td>");
            w.Write(@"<td></td>");
            w.Write(@"<td></td>");
            w.Write(@"<td></td>");
            w.Write("</tr>");

            #endregion
        }

        /// <summary>
        /// Процедура вывода данных в "тело" таблицы
        /// </summary>
        /// <param name="w">Поток вывода</param>
        /// <param name="dv">Источник данных</param>
        /// <param name="sumNDS">Параметр, в который будет насчитан НДС по сумме для каждой записи</param>
        /// <param name="sumNDSE">Параметр, в который будет насчитан НДС по сумме сутрудника для каждой записи</param>
        private void RenderTableBody(StringWriter w, DataView dv, ref decimal calcSum, ref decimal calcSumE)
        {
            DateTime dTime;
            string _dTime = "";

            decimal sum = 0M;
            decimal sumE = 0M;

            string title = GetTitleByColor();
            for (int i = 0; i < dv.Count; i++)
            {
                w.Write("<tr");
                if (i%2 != 0)
                    w.Write(" style='background:#efefef;'");
                w.Write(">");
                if (dv[i]["НачалоРазговора"] == null || dv[i]["НачалоРазговора"].ToString().Length == 0)
                    _dTime = "";
                else
                {
                    dTime = (DateTime) dv[i]["НачалоРазговора"];
                    _dTime = dTime.ToString("dd.MM.yy HH:mm:ss");
                }

                w.Write("<td class='tdR' noWrap>{0}</td>", _dTime);
                w.Write("<td class='tdC'>{0}</td>", dv[i]["Роуминг"]);
                w.Write("<td class='tdC'>{0}</td>", dv[i]["Телефон"]);
                w.Write("<td class='tdC'>{0}</td>", dv[i]["Тип"]);
                w.Write("<td >{0}</td>", dv[i]["Услуга"]);
                w.Write("<td >{0}</td>", dv[i]["Описание"]);
                w.Write("<td class='tdR' noWrap>{0}</td>",
                        Convert.Second2TimeFormat(int.Parse(dv[i]["Секунд"].ToString())));
                w.Write("<td class='tdR' noWrap>{0}</td>", dv[i]["Килобайт"]);
                w.Write("<td class='tdR' noWrap style='{0}' {1}>", _qsParams["Color"], title);


                sum = Convert.Round((decimal) dv[i]["Сумма"], _scale);
                sumE = Convert.Round((decimal) dv[i]["СуммаСотрудника"], _scale);

                if (
                    System.Convert.ToInt32(_qsParams["NDSAllSumm"])
                        .Equals(System.Convert.ToInt32(TarifMobilPhone.СalculationNDS.OnCostOfEachCall)))
                {
                    sum = Convert.Round(sum*(1 + _ndsStavka), _scale);
                    sumE = Convert.Round(sumE*(1 + _ndsStavka), _scale);
                }

                calcSum += sum;
                calcSumE += sumE;

                RenderNumber(w, Convert.Decimal2Str(sumE, _scale), _scale);
                w.Write("</td>");
                w.Write("<td class='tdR' style='{0}' {1}>", _qsParams["Color"], title);
                RenderNumber(w, Convert.Decimal2Str(sum, _scale), _scale);
                w.Write("</td>");

                w.Write("</tr>");
            }
        }

        private string GetTitleByColor()
        {
            string[] colorStyle = _qsParams["Color"].Split(':');
            if (colorStyle.Length < 2) return "";
            string color = colorStyle[1].Replace(";", "").Trim();
            string title = "title='{0}'";

            if (color.Equals(_colorWhite))
                title = string.Format(title, TitleWhite);
            else if (color.Equals(_colorGray))
                title = string.Format(title, TitleGray);
            else
                title = string.Format(title, TitleOrange);
            return title;
        }

        /// <summary>
        /// Пороцедура вывода "подвала" таблицы с итоговыми суммами
        /// </summary>
        /// <param name="w">Поток вывода</param>
        /// <param name="dv">Источник данных</param>
        /// <param name="sumNDS">Насчитанный на каждую запись НДС на сумму</param>
        /// <param name="sumNDSE">Насчитанный на каждую запись НДС на сумму сотрудника</param>
        private void RenderTableFooter(StringWriter w, DataView dv, decimal sum, decimal sumE)
        {
            decimal sec = _sumList["Секунд"];
            decimal kb = _sumList["Килобайт"];
     
            w.Write("<tr  class='gridHeader'>");
            w.Write("<td></td>");
            w.Write("<td></td>");
            w.Write("<td></td>");
            w.Write("<td></td>");
            w.Write("<td></td>");
            w.Write("<td align='right'>{0}:</td>", HttpUtility.HtmlEncode(TMD_TblFooter));
            w.Write("<td class='tdR' noWrap>{0}</td>", Convert.Second2TimeFormat((int) sec));
            w.Write("<td class='tdR' noWrap>");
            RenderNumber(w, Convert.Decimal2Str(kb, 0), 0);
            w.Write("</td>");
            w.Write("<td class='tdR' noWrap>");
            RenderNumber(w, Convert.Decimal2Str(sumE, _scale), _scale);
            w.Write("</td>");
            w.Write("<td class='tdR' noWrap>");
            RenderNumber(w, Convert.Decimal2Str(sum, _scale), _scale);
            w.Write("</td>");
            w.Write("</tr>");
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
                sb.AppendFormat("<font style='color:red;'>{0}</font>", TM_OutSimData);
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

        #endregion

        #region RefreshTable

        /// <summary>
        /// Процедура обновления содержимого таблицы 
        /// </summary>
        private void RefreshTableBody()
        {
            RefreshTableBody(true, false, "");
        }

        /// <summary>
        /// Процедура обновления содержимого таблицы 
        /// </summary>
        /// <param name="refreshFromDB">Обновлять из базы данных</param>
        /// <param name="toFirstPage">Переходить на первую страницу</param>
        /// <param name="sort">Порядок сортировки</param>
        private void RefreshTableBody(bool refreshFromDB, bool toFirstPage, string sort)
        {
            var localParams = new StringCollection();
            var w = new StringWriter();

            //if (toFirstPage) Pager.CurrentPageNumber = 1;

            if (refreshFromDB)
            {
                if (txtPhone.Value.Length > 0)
                    localParams.Add("Телефон LIKE '" + txtPhone.Value + "%'");
                
                if (cbRouming.Value.Length >0)
                    localParams.Add("Роуминг = '" + cbRouming.Value + "'");

                if (cbType.Value.Length != 0)
                    localParams.Add("Тип = '" + cbType.Value + "'");
                
                if (cbDescription.Value.Length != 0)
                    localParams.Add("Описание = '" + cbDescription.Value + "'");

                if (cbService.Value.Length != 0)
                    localParams.Add("Услуга = '" + cbService.Value + "'");

                try
                {
                    _dvData = GetData(localParams, sort);
                }
                catch (Exception ex)
                {
                    ShowMessage(System.Environment.NewLine + ex.Message, "Ошибка при получении данных");
                    RestoreCursor();
                    return;
                }
                
            }
            decimal sumNDS = 0M;
            decimal sumNDSE = 0M;
            RenderTableBody(w, _dvData, ref sumNDS, ref sumNDSE);
            JS.Write("set_tBody('{0}');", HttpUtility.JavaScriptStringEncode(w.ToString()));
            RefreshTableFoot(sumNDS, sumNDSE);
            JS.Write("$(\".btn\").css(\"cursor\", \"pointer\");");
        }

        /// <summary>
        /// Процедура обновления "подвала" таблицы
        /// </summary>
        /// <param name="sumNDS">Насчитанный на каждую запись НДС на сумму</param>
        /// <param name="sumNDSE">Насчитанный на каждую запись НДС на сумму сотрудника</param>
        private void RefreshTableFoot(decimal sumNDS, decimal sumNDSE)
        {
            var w = new StringWriter();
            RenderTableFooter(w, _dvData, sumNDS, sumNDSE);
            JS.Write("set_tFoot('{0}');", HttpUtility.JavaScriptStringEncode(w.ToString()));
        }

        #endregion

        #region Handler

        #region CheckBox

        /// <summary>
        /// Процедура формирования словаря, с колонками по которым производится группировка
        /// </summary>
        /// <param name="column">Название колонки</param>
        /// <param name="value">Значение</param>
        private void CheckBoxChanged(string column, string value)
        {
            _groupByList[column] = value.Equals("1");
            RefreshTableBody(true, true, _sqSort.ReverseListValues);
        }

        /// <summary>
        /// Обработчик события изменения значения группировки: Телефон
        /// </summary>
        /// <param name="sender">Контрол</param>
        /// <param name="e">Значение</param>
        private void Event_chPhone_Changed(object sender, ProperyChangedEventArgs e)
        {
            JS.Write("ServerOnChange('checkbox', '{0}', '{1}');", HttpUtility.JavaScriptStringEncode("Телефон"),
                     HttpUtility.JavaScriptStringEncode(e.NewValue));
            RestoreCursor();
        }

        /// <summary>
        ///  Обработчик события изменения значения группировки: Тип
        /// </summary>
        /// <param name="sender">Контрол</param>
        /// <param name="e">Значение</param>
        private void Event_chType_Changed(object sender, ProperyChangedEventArgs e)
        {
            JS.Write("ServerOnChange('checkbox', '{0}', '{1}');", HttpUtility.JavaScriptStringEncode("Тип"),
                     HttpUtility.JavaScriptStringEncode(e.NewValue));
            RestoreCursor();
        }


        /// <summary>
        ///  Обработчик события изменения значения группировки: Роуминг
        /// </summary>
        /// <param name="sender">Контрол</param>
        /// <param name="e">Значение</param>
        private void Event_chRouming_Changed(object sender, ProperyChangedEventArgs e)
        {
            JS.Write("ServerOnChange('checkbox', '{0}', '{1}');", HttpUtility.JavaScriptStringEncode("Роуминг"),
               HttpUtility.JavaScriptStringEncode(e.NewValue));
            RestoreCursor();
        }

        
        /// <summary>
        ///  Обработчик события изменения значения группировки: Описание услуги тарификации
        /// </summary>
        /// <param name="sender">Контрол</param>
        /// <param name="e">Значение</param>
        private void Event_chDescription_Changed(object sender, ProperyChangedEventArgs e)
        {
            JS.Write("ServerOnChange('checkbox', '{0}', '{1}');", HttpUtility.JavaScriptStringEncode("Описание"),
                     HttpUtility.JavaScriptStringEncode(e.NewValue));
            RestoreCursor();
        }

        /// <summary>
        ///  Обработчик события изменения значения группировки: Услуга
        /// </summary>
        /// <param name="sender">Контрол</param>
        /// <param name="e">Значение</param>
        private void Event_chService_Changed(object sender, ProperyChangedEventArgs e)
        {
            JS.Write("ServerOnChange('checkbox', '{0}', '{1}');", HttpUtility.JavaScriptStringEncode("Услуга"),
                     HttpUtility.JavaScriptStringEncode(e.NewValue));
            RestoreCursor();
        }

        #endregion

        #region ComboBox

        /// <summary>
        ///  Обработчик события изменения значения фильтрации: Тип
        /// </summary>
        /// <param name="sender">Контрол</param>
        /// <param name="e">Значение</param>
        private void Event_cbType_Changed(object sender, ProperyChangedEventArgs e)
        {
            JS.Write("ServerOnChange('combobox');");
        }

        /// <summary>
        ///  Обработчик события изменения значения фильтрации: Роуминг
        /// </summary>
        /// <param name="sender">Контрол</param>
        /// <param name="e">Значение</param>
        private void Event_cbRouming_Changed(object sender, ProperyChangedEventArgs e)
        {
            JS.Write("ServerOnChange('combobox');");
        }
        

        /// <summary>
        ///  Обработчик события изменения значения фильтрации: Описание услуги тарификации
        /// </summary>
        /// <param name="sender">Контрол</param>
        /// <param name="e">Значение</param>
        private void Event_cbDescription_Changed(object sender, ProperyChangedEventArgs e)
        {
            JS.Write("ServerOnChange('combobox');");
        }

        /// <summary>
        ///  Обработчик события изменения значения фильтрации: Услуга
        /// </summary>
        /// <param name="sender">Контрол</param>
        /// <param name="e">Значение</param>
        private void Event_cbService_Changed(object sender, ProperyChangedEventArgs e)
        {
            JS.Write("ServerOnChange('combobox');");
        }

        #endregion

        #region TextBox

        /// <summary>
        ///  Обработчик события изменения значения фильтрации: Телефон
        /// </summary>
        /// <param name="sender">Контрол</param>
        /// <param name="e">Значение</param>
        private void Event_txtPhone_Changed(object sender, ProperyChangedEventArgs e)
        {
            JS.Write("ServerOnChange('textbox');");
        }

        #endregion

        #endregion

        #region Dynamic control

        /// <summary>
        /// Динамичесоке создание контрола типа CheckBox
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="text">Название</param>
        /// <param name="hadler">Указатель на событие при изменении значения</param>
        /// <returns>Kesco.Lib.Web.Controls.V4.CheckBox</returns>
        private CheckBox CreateCheckBox(string id, string text, ChangedEventHandler hadler)
        {
            var ctrl = new CheckBox();
            ctrl.Text = text;
            ctrl.LabelFor = false;
            ctrl.ID = id;
            ctrl.HtmlID = id;
            ctrl.V4Page = this;
            V4Controls.Add(ctrl);
            ctrl.V4OnInit();
            ctrl.Changed += hadler;

            return ctrl;
        }

        /// <summary>
        /// Динамичесоке создание контрола типа TextBox
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="h">Высота</param>
        /// <param name="w">Ширина</param>
        /// <param name="hadler">Указатель на событие при изменении значения</param>
        /// <returns>Kesco.Lib.Web.Controls.V4.ComboBox</returns>
        private ComboBox CreateComboBox(string id, int h, int w, ChangedEventHandler hadler)
        {
            var ctrl = new ComboBox();
            ctrl.ID = id;
            ctrl.HtmlID = id;
            ctrl.V4Page = this;
            ctrl.Height = new Unit(h + "px");
            ctrl.Width = new Unit(w + "px");
            V4Controls.Add(ctrl);
            ctrl.V4OnInit();
            ctrl.Changed += hadler;

            return ctrl;
        }

        /// <summary>
        /// Динамичесоке создание контрола типа ComboBox
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="h">Высота</param>
        /// <param name="w">Ширина</param>
        /// <param name="hadler">Указатель на событие при изменении значения</param>
        /// <returns>Kesco.Lib.Web.Controls.V4.TextBox</returns>
        private TextBox CreateTextBox(string id, int h, int w, ChangedEventHandler hadler)
        {
            var ctrl = new TextBox();
            ctrl.ID = id;
            ctrl.HtmlID = id;
            ctrl.V4Page = this;
            ctrl.Height = new Unit(h + "px");
            ctrl.Width = new Unit(w + "px");
            V4Controls.Add(ctrl);
            ctrl.V4OnInit();
            ctrl.Changed += hadler;

            return ctrl;
        }

        /// <summary>
        /// Процедура заполнения контрола типа ComboBox
        /// </summary>
        /// <param name="cb">Контрол</param>
        /// <param name="dv">Истичник данных</param>
        /// <param name="f">Название поля из источника данных</param>
        private void FillComboBox(ComboBox cb, DataView dv, string f)
        {
            var results = from r in dv.ToTable().AsEnumerable()
                          group r by new {Field = r.Field<string>(f)}
                          into groupField
                          orderby groupField.Key.Field
                          select new
                                     {
                                         groupField.Key.Field,
                                     };

            foreach (var d in results)
                cb.Items.Add(d.Field, d.Field);
        }

        #endregion

        #region Pager

        //private void SetDefaultPagerSettings()
        //{
        //    Pager.CurrentPageNumber = 1;
        //    Pager.MaxPageNumber = 1;
        //    Pager.RowsPerPage = 50;
        //}

        //private void Pager_CurrentPageChanged(object sender, EventArgs e)
        //{
        //    RefreshTableBody();
        //}

        //private void Pager_RowsPerPageChanged(object sender, EventArgs e)
        //{
        //    RefreshTableBody();
        //}

        #endregion

        #region Sort

        /// <summary>
        /// Процедура сортировки по указанной колонке
        /// </summary>
        /// <param name="column">Название колонки</param>
        private void SortDataByColumn(string column)
        {
            string columnDESC = column + " DESC";
            if (_sqSort.Count > 0)
            {
                if (_sqSort.Last().Equals(column))
                {
                    _sqSort.Remove(column);
                    column = columnDESC;
                }
            }

            _sqSort.Remove(column);
            _sqSort.Remove(columnDESC);

            _sqSort.Enqueue(column);


            RefreshTableBody(true, false, _sqSort.ReverseListValues);
        }

        /// <summary>
        /// Формирование ссылок в заголовке таблицы для сортироки по указанным полям
        /// </summary>
        /// <param name="sortColumn">Название колонки, по которой будет осуществляться сортировка</param>
        /// <param name="displayColumn">Название колонки, которое будет отображаться на экране</param>
        /// <returns>HTML-контент</returns>
        private string RenderSortableColumnLink(string sortColumn, string displayColumn)
        {
            var sb = new StringBuilder();

            sb.AppendFormat(@"<u class='btn' onclick=""SortData('{0}');"">", sortColumn);
            sb.Append(displayColumn);
            sb.Append("</u>");

            return sb.ToString();
        }

        #endregion

        
    }
}