using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Web;
using System.Web.Hosting;
using Kesco.Lib.DALC;
using Kesco.Lib.Entities;
using Kesco.Lib.Localization;
using Kesco.Lib.Web;
using Kesco.Lib.Web.Controls.V4;
using Kesco.Lib.Web.Controls.V4.Common;
using Convert = Kesco.Lib.ConvertExtention.Convert;
using Utils = Kesco.Lib.ConvertExtention;
using Kesco.Lib.Web.DBSelect.V4;
using Kesco.Lib.Web.Settings;

namespace Kesco.App.Web.TarifMobilPhone
{
    /// <summary>
    /// Перечислитель статусов нотификации
    /// </summary
    public enum СalculationNDS
    {
        /// <summary>
        /// Не известно, т.к не условий договора
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// НДС должен насчитываться на каждый звонок
        /// </summary>
        OnCostOfEachCall = 1,
        /// <summary>
        /// НДС должен насчитываться на общую сумму по договору
        /// </summary>
        OnTotalSumOfContract = 2,
         /// <summary>
        /// Не трубуется начисление НДС
        /// </summary>
        NotRequired = 3
    }

    /// <summary>
    /// Класс формы default.aspx - основная форма приложения, Тарификация: Мобильная связь
    /// </summary>
    public partial class DefaultPage : Page
    {
       
        protected override string HelpUrl { get; set; }
       
        /// <summary>
        /// Переменная для хранения текущего открытого уровня
        /// </summary>
        public string CurrentLevel = "1";
        /// <summary>
        /// Менеджер ресурсов для локализации
        /// </summary>
        public ResourceManager Resx = Resources.Resx;

        
    
        #region Protected Var
        
        /// <summary>
        /// Строка для передачи на клиента, соответствующего значения из ресурсного файла
        /// </summary>
        protected string LegendTitle = "";
        /// <summary>
        /// Строка для передачи на клиента, соответствующего значения из ресурсного файла
        /// </summary>
        protected string TitleGray = "";
        /// <summary>
        /// Строка для передачи на клиента, соответствующего значения из ресурсного файла
        /// </summary>
        protected string TitleOrange = "";
        /// <summary>
        /// Строка для передачи на клиента, соответствующего значения из ресурсного файла
        /// </summary>
        protected string TitleWhite = "";
        
        /// <summary>
        ///Переменная для получения значения надписи из ресурсного файла в зависимости от языка пользователя
        /// </summary>
        protected string TM_AllYear = "";

        /// <summary>
        ///Переменная для получения значения надписи из ресурсного файла в зависимости от языка пользователя
        /// </summary>
        protected string TM_FOpenMonth = "";
        /// <summary>
        ///Переменная для получения значения надписи из ресурсного файла в зависимости от языка пользователя
        /// </summary>
        protected string TM_FYear = "";
        /// <summary>
        ///Переменная для получения значения надписи из ресурсного файла в зависимости от языка пользователя
        /// </summary>
        protected string TM_FMonth = "";
        /// <summary>
        ///Переменная для получения значения надписи из ресурсного файла в зависимости от языка пользователя
        /// </summary>
        protected string TM_FDogovor = "";
        /// <summary>
        ///Переменная для получения значения надписи из ресурсного файла в зависимости от языка пользователя
        /// </summary>
        protected string TM_FEmployee = "";
        /// <summary>
        ///Переменная для получения значения надписи из ресурсного файла в зависимости от языка пользователя
        /// </summary>
        protected string TM_FPhone = "";
        /// <summary>
        ///Переменная для получения значения надписи из ресурсного файла в зависимости от языка пользователя
        /// </summary>
        protected string TM_ClearFilter = "";
        /// <summary>
        ///Переменная для получения значения надписи из ресурсного файла в зависимости от языка пользователя
        /// </summary>
        protected string TM_FilterApply = "";
        /// <summary>
        ///Переменная для получения значения надписи из ресурсного файла в зависимости от языка пользователя
        /// </summary>
        protected string TM_Rpt1 = "";
        /// <summary>
        ///Переменная для получения значения надписи из ресурсного файла в зависимости от языка пользователя
        /// </summary>
        protected string TM_Rpt2 = "";
        /// <summary>
        ///Переменная для получения значения надписи из ресурсного файла в зависимости от языка пользователя
        /// </summary>
        protected string TM_Rpt3 = "";
        /// <summary>
        ///Переменная для получения значения надписи из ресурсного файла в зависимости от языка пользователя
        /// </summary>
        protected string TM_Rpt4 = "";
        /// <summary>
        ///Переменная для получения значения надписи из ресурсного файла в зависимости от языка пользователя
        /// </summary>
        protected string TM_Rpt5 = "";
        /// <summary>
        ///Переменная для получения значения надписи из ресурсного файла в зависимости от языка пользователя
        /// </summary>
        protected string TM_TblColumn2 = "";
        /// <summary>
        ///Переменная для получения значения надписи из ресурсного файла в зависимости от языка пользователя
        /// </summary>
        protected string TM_TblColumn3 = "";
        /// <summary>
        ///Переменная для получения значения надписи из ресурсного файла в зависимости от языка пользователя
        /// </summary>
        protected string TM_TblColumn4_0 = "";
        /// <summary>
        ///Переменная для получения значения надписи из ресурсного файла в зависимости от языка пользователя
        /// </summary>
        protected string TM_TblColumn4_1 = "";
        /// <summary>
        ///Переменная для получения значения надписи из ресурсного файла в зависимости от языка пользователя
        /// </summary>
        protected string TM_FDataEmptyMonth = "";
        /// <summary>
        ///Переменная для получения значения надписи из ресурсного файла в зависимости от языка пользователя
        /// </summary>
        protected string TM_FDataEmptyDogovor = "";
        /// <summary>
        ///Переменная для получения значения надписи из ресурсного файла в зависимости от языка пользователя
        /// </summary>
        protected string TM_FDataEmptyEmployee = "";
        /// <summary>
        ///Переменная для получения значения надписи из ресурсного файла в зависимости от языка пользователя
        /// </summary>
        protected string TM_FDataEmptyPhone = "";
        /// <summary>
        ///Переменная для получения значения надписи из ресурсного файла в зависимости от языка пользователя
        /// </summary>
        protected string TM_Title = "";
        /// <summary>
        ///Переменная для получения значения надписи из ресурсного файла в зависимости от языка пользователя
        /// </summary>
        protected string TM_RptDetail = "";

        /// <summary>
        ///Переменная для получения значения надписи из ресурсного файла в зависимости от языка пользователя
        /// </summary>
        protected string TM_Empty = "";

        /// <summary>
        ///Переменная для получения значения надписи из ресурсного файла в зависимости от языка пользователя
        /// </summary>
        protected string TM_Wait = "";

        /// <summary>
        ///Переменная для получения значения надписи из ресурсного файла в зависимости от языка пользователя
        /// </summary>
        private string TM_OutSimData = "";

        /// <summary>
        ///Переменная для получения значения надписи из ресурсного файла в зависимости от языка пользователя
        /// </summary>
        private string TM_Issued = "";
        
        /// <summary>
        ///Переменная для получения значения надписи из ресурсного файла в зависимости от языка пользователя
        /// </summary>
        private string TM_Withdrawn = "";
        

        /// <summary>
        ///Переменная для получения значения надписи из ресурсного файла в зависимости от языка пользователя
        /// </summary>
        private string TM_SimInfo = "";

        
        

        /// <summary>
        ///Переменная для получения значения надписи из ресурсного файла в зависимости от языка пользователя
        /// </summary>
        protected string Title_Help = "";
        
        #endregion

        
        #region Private Var

        
        /// <summary>
        /// Список валют
        /// </summary>
        private DataTable _currencies;
        
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

        #endregion

  
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
                chOpenMonth.Changed += Event_chOpenMonth_Changed;
                cbYear.Changed += Event_cbYear_Changed;
                cbMonth.Changed += Event_cbMonth_Changed;
                cbDogovor.Changed += Event_cbDogovor_Changed;
                cbEmployee.Changed += Event_cbEmployee_Changed;
                cbPhone.Changed += Event_cbPhone_Changed;

                ResxResourceSet();
                FillComboBox();
                chOpenMonth.Checked = false;
                OpenMonthChanged("0", false);

                FillCurrencies();
                HelpUrl = "hlp/help.htm";
               
            }
        }
        
        /// <summary>
        /// Обработчик команд V4, которые посылаются с клиента на сервер:
        /// <list type="bullet">
        /// <item>
        /// <description>OpentRpt - открытие отчета, в зависимости от переданного параметра</description>
        /// </item>
        /// <item>
        /// <description>FilterClear - очистка таблицы с результами фильтрации</description>
        /// </item>
        /// <item>
        /// <description>FilterApply - применение фильтра, вывод полученных данных клиенту</description>
        /// </item>
        /// <item>
        /// <description>SetLevel - фиксация текущего открытого уровня в дереве</description>
        /// </item>
        /// <item>
        /// <description>checkbox - асинхронный вызов события изменения значения в контроле "Незакрытый месяц"</description>
        /// </item>
        /// <item>
        /// <description>combobox - асинхронный вызов события изменения значения в контролах типа combobox</description>
        /// </item>
        /// </list>
        /// </summary>
        /// <param name="cmd">Название команды</param>
        /// <param name="param">Коллекция параметров</param>
        protected override void ProcessCommand(string cmd, NameValueCollection param)
        {
            switch (cmd)
            {
                case "RemoveItem":
                   // RemoveItem(param["id"], param["type"]);
                    break;
                case "OpentRpt":
                    RenderOpenRptLink(param["inx"]);
                    break;
                //case "OpentBln":
                //    OpentBln();
                //    break;
                case "FilterClear":
                    FilterClear();
                    break;
                case "FilterApply":
                    FilterApply();
                    break;
                case "SetLevel":
                    CurrentLevel = param["CurrentLevel"];
                    break;
                case "RenderSim":
                    RenderSim(param["id"], param["data"]);
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
            
            TitleGray = Resx.GetString("TM_TitleGray2");
            TitleWhite = Resx.GetString("TM_TitleWhite");
            TitleOrange = Resx.GetString("TM_TitleOrange");

            TM_FOpenMonth = Resx.GetString("TM_FOpenMonth");
            TM_FYear = Resx.GetString("TM_FYear");
            TM_FMonth = Resx.GetString("TM_FMonth");

            TM_FDogovor = Resx.GetString("TM_FDogovor");
            TM_FEmployee = Resx.GetString("TM_FEmployee");
            TM_FPhone = Resx.GetString("TM_FPhone");
            TM_ClearFilter = Resx.GetString("TM_ClearFilter");
            TM_FilterApply = Resx.GetString("TM_FilterApply");
            TM_Rpt1 = Resx.GetString("TM_Rpt1");
            TM_Rpt2 = Resx.GetString("TM_Rpt2");
            TM_Rpt3 = Resx.GetString("TM_Rpt3");
            TM_Rpt4 = Resx.GetString("TM_Rpt4");
            TM_Rpt5 = Resx.GetString("TM_Rpt5");
            TM_TblColumn2 = Resx.GetString("TM_TblColumn2");
            TM_TblColumn3 = Resx.GetString("TM_TblColumn3");
            TM_TblColumn4_0 = Resx.GetString("TM_TblColumn4_0");
            TM_TblColumn4_1 = Resx.GetString("TM_TblColumn4_1");
            TM_FDataEmptyMonth = Resx.GetString("TM_FDataEmptyMonth");
            TM_FDataEmptyDogovor = Resx.GetString("TM_FDataEmptyDogovor");
            TM_FDataEmptyEmployee = Resx.GetString("TM_FDataEmptyEmployee");
            TM_FDataEmptyPhone = Resx.GetString("TM_FDataEmptyPhone");
            TM_Title = Resx.GetString("TM_Title");
            TM_RptDetail = Resx.GetString("TM_RptDetail");
            TM_Empty = Resx.GetString("TM_Empty");
            TM_Wait = Resx.GetString("TM_Wait");
            TM_OutSimData = Resx.GetString("TM_OutSimData");
            TM_Issued = Resx.GetString("TM_Issued");
            TM_Withdrawn = Resx.GetString("TM_Withdrawn");
            TM_SimInfo = Resx.GetString("TM_SimInfo");
            Title_Help = Resx.GetString("Title_Help");
            TM_AllYear = Resx.GetString("TM_AllYear");
        }

       

        #endregion

       
        #region Ссылки на странице

        /*
        /// <summary>
        /// Генерит ссылку на странице для открытия детализации по всем договорам Билайна
        /// </summary>
        private void OpentBln()
        {
            string pOpenMonth = chOpenMonth.Value.Length == 0 ? "0" : chOpenMonth.Value;
            string pYear = pOpenMonth.Equals("1") ? "0" : cbYear.Value;
            string pMonth = pOpenMonth.Equals("1") ? "0" : cbMonth.Value;
            string pDogovor = null;
            string pDogovorT = "билайн";
            string pUser = null;
            string pPhone = "";

            JS.Write("openDetailItem('{0}','{1}','{2}','{3}','{4}','{5}','{6}', '1', '{7}','{8}', '{9}', '{10}', '{11}', '{12}');"
                , pOpenMonth
                , pYear
                , pMonth
                , pDogovor 
                , HttpUtility.JavaScriptStringEncode(pDogovorT)
                , pUser
                , pPhone
                , 1
                , 0.18
                , 0
                , 2
                , HttpUtility.JavaScriptStringEncode(string.Format("background:{0};", colorWhite))
                , HttpUtility.JavaScriptStringEncode("")
                );
        }
        */

        /// <summary>
        /// Генерит ссылку на странице для открытия соответствующего отчета
        /// </summary>
        /// <param name="p">Код отчета</param>
        private void RenderOpenRptLink(string p)
        {
            string year = cbYear.Value.Length == 0 ? "-1" : cbYear.Value;
            string month = cbMonth.Value.Length == 0 ? "-1" : cbMonth.Value;
            string dogovor = cbDogovor.Value.Length == 0 ? "-1" : cbDogovor.Value;

            if (chOpenMonth.Value.Equals("1"))
            {
                year = "0";
                month = "0";
            }

            string url = Config.uri_Report + "?/Accounting/AccountingMobil" + p +
                         "&rc:parameters=false&rs:ClearSession=true";

            url += "&Year=" + year;
            url += "&Month=" + month;
            url += "&Dogovor=" + dogovor;

            JS.Write("v4_windowOpen('{0}');", HttpUtility.JavaScriptStringEncode(url));
        }
        
        #endregion
        

        #region Handlers

        /// <summary>
        /// Обработчик изменения значения флага незакрытого месяца
        /// <seealso cref="OpenMonthChanged"/>
        /// </summary>
        /// <see cref="OpenMonthChanged"/>
        /// <param name="sender">Контрол</param>
        /// <param name="e">Значение</param>
        private void Event_chOpenMonth_Changed(object sender, ProperyChangedEventArgs e)
        {
            OpenMonthChanged(e.NewValue, false);
        }

        /// <summary>
        /// Функция, которая вызывается в обработчике изменения значения флага незакрытого месяца
        /// </summary>
        /// <param name="v">Значение</param>
        /// <param name="filterApply">Нужно ли применить фильтр при изменении значения</param>
        private void OpenMonthChanged(string v, bool filterApply)
        {
            DisplayRowsData(v);
            FillComboBoxDogovor();
            FillComboBoxEmployee();
            FillComboBoxPhone();
            ResultClear();
            //if (filterApply) FilterApply();
        }

        /// <summary>
        /// Обработчик изменения значения в списке с годами тарификации
        /// <seealso cref="YearChanged"/>
        /// </summary>
        /// <param name="sender">Контрол</param>
        /// <param name="e">Значение</param>
        private void Event_cbYear_Changed(object sender, ProperyChangedEventArgs e)
        {
            YearChanged();
        }
        /// <summary>
        ///  Функция, которая вызывается в обработчике изменения значения списка с годами тарификации
        /// </summary>
        private void YearChanged()
        {
            FillComboBoxDogovor();
            FillComboBoxEmployee();
            FillComboBoxPhone();
            ResultClear();
            //FilterApply();
        }

        /// <summary>
        /// Обработчик изменения значения в списке с месяцами
        /// <seealso cref="MonthChanged"/>
        /// </summary>
        /// <param name="sender">Контрол</param>
        /// <param name="e">Значение</param>
        private void Event_cbMonth_Changed(object sender, ProperyChangedEventArgs e)
        {
            MonthChanged();
        }
        /// <summary>
        ///  Функция, которая вызывается в обработчике изменения значения списка с месяцами
        /// </summary>
        private void MonthChanged()
        {
            FillComboBoxDogovor();
            FillComboBoxEmployee();
            FillComboBoxPhone();
            ResultClear();
            //FilterApply();
        }

     
        /// <summary>
        /// Обработчик изменения значения в списке с договорами
        /// <seealso cref="DogovorChanged"/>
        /// </summary>
        /// <param name="sender">Контрол</param>
        /// <param name="e">Значение</param>
        private void Event_cbDogovor_Changed(object sender, ProperyChangedEventArgs e)
        {
            DogovorChanged();
        }
        /// <summary>
        ///  Функция, которая вызывается в обработчике изменения значения списка с договорами
        /// </summary>
        private void DogovorChanged()
        {
            FillComboBoxEmployee();
            FillComboBoxPhone();
            ResultClear();
            //FilterApply();
        }

        /// <summary>
        /// Обработчик изменения значения в списке с сотрудниками
        /// <seealso cref="EmployeeChanged"/>
        /// </summary>
        /// <param name="sender">Контрол</param>
        /// <param name="e">Значение</param>
        private void Event_cbEmployee_Changed(object sender, ProperyChangedEventArgs e)
        {
            EmployeeChanged();
        }
        /// <summary>
        ///  Функция, которая вызывается в обработчике изменения значения списка с сотрудниками
        /// </summary>
        private void EmployeeChanged()
        {
            FillComboBoxPhone();
            ResultClear();
            //FilterApply();
        }

        /// <summary>
        /// Обработчик изменения значения в списке с телефонами
        /// </summary>
        /// <seealso cref="PhoneChanged"/>
        /// <param name="sender">Контрол</param>
        /// <param name="e">Значение</param>
        private void Event_cbPhone_Changed(object sender, ProperyChangedEventArgs e)
        {
            PhoneChanged();
        }
        /// <summary>
        ///  Функция, которая вызывается в обработчике изменения значения списка с телефонами
        /// </summary>
        private void PhoneChanged()
        {
            ResultClear();
            //FilterApply();
        }
        
        #endregion

        
        #region FillList

        /// <summary>
        /// Процедура, скрывающая/показывающая контролы выбора года и месяца в зависимости от значения флага-Незакрытый месяц
        /// </summary>
        /// <param name="p">Значение контрола chOpenMonth</param>
        private void DisplayRowsData(string p)
        {
            bool disp = p.Equals("1") ? false : true;
            Display("tr2", disp);
            Display("tr3", disp);
        }

        /// <summary>
        /// Процедура получения списка валют
        /// </summary>
        private void FillCurrencies()
        {
            Dictionary<string, object> sqlParams = GetSQLParams(-1);
            _currencies = DBManager.GetData(SQL_Queries.SQL_СписокВалют, Config.DS_person, CommandType.Text, sqlParams);
        }

        /// <summary>
        /// Общая процедура для вызова процедур заполнения списка годов и месяцев
        /// </summary>
        private void FillComboBox()
        {
            FillComboBoxYear();
            FillComboBoxMonth();
        }

        /// <summary>
        /// Процедура заполнения списка годов тарификации
        /// </summary>
        private void FillComboBoxYear()
        {
            cbYear.EmptyValueExist = false;
            cbYear.Items.Clear();
            cbYear.DataItems.Clear();
            cbYear.KeyField = "Год";
            cbYear.ValueField = "ГодТекст";
            DataTable dt = DBManager.GetData(SQL_Queries.SQL_ПервыйГодТарификации, Config.DS_accounting_phone,
                                             CommandType.Text, null);
            cbYear.FillList = dt.AsEnumerable;

            if (cbYear.Contains(DateTime.Today.Year.ToString()))
                cbYear.Value = DateTime.Today.Year.ToString();
            else
            {
                if (dt.Rows.Count > 0)
                    cbYear.Value = dt.Rows[dt.Rows.Count - 1][0].ToString();
            }
        }

        /// <summary>
        /// Процедура заполнения списка месяцев тарификации
        /// </summary>
        private void FillComboBoxMonth()
        {
            cbMonth.Items.Clear();
            cbMonth.EmptyValueText = string.Format("- {0} -", TM_AllYear);
            cbMonth.DataItems.Clear();
            cbMonth.Value = "";
            string month = "";
            CultureInfo culture = new CultureInfo(CurrentUser.Language);
            for (int i = 1; i < 13; i++)
            {
                month = culture.DateTimeFormat.GetMonthName(i);
                cbMonth.Items.Add(i.ToString(), month);
            }

            if (!V4IsPostBack)
            {
                if (cbYear.Value.Length > 0 && cbYear.Value == DateTime.Today.Year.ToString())
                {
                    if (DateTime.Today.Month == 1)
                    {
                        cbMonth.Value = "12";
                        cbYear.Value = (DateTime.Today.Year - 1).ToString();
                    }
                    else
                        cbMonth.Value = (DateTime.Today.Month - 1).ToString();
                }
            }
        }

        /// <summary>
        /// Процедура заполнения списка договоров связи
        /// </summary>
        private void FillComboBoxDogovor()
        {
            string oldVal = cbDogovor.Value;
            cbDogovor.EmptyValueText = string.Format("- {0} -", TM_FDataEmptyDogovor);
            cbDogovor.Items.Clear();
            cbDogovor.DataItems.Clear();
            cbDogovor.Value = "";
            cbDogovor.KeyField = "КодДоговора";
            cbDogovor.ValueField = "Договор";
            Dictionary<string, object> sqlParams = GetSQLParams(1);
            DataTable dt = DBManager.GetData(SQL_Queries.SQL_ДоговораПоКоторымБылаТарификация,
                                             Config.DS_accounting_phone, CommandType.Text, sqlParams);
            cbDogovor.FillList = dt.AsEnumerable;

            if (V4IsPostBack)
            {
                EnumerableRowCollection<DataRow> result = from r in dt.AsEnumerable()
                                                          where
                                                              oldVal.Length == 0 ||
                                                              (oldVal.Length > 0 &&
                                                               r.Field<int>("КодДоговора") == int.Parse(oldVal))
                                                          select r;

                DataView dv = result.AsDataView();

                if (dv.Count > 0)
                {
                    if (oldVal.Length > 0)
                        cbDogovor.Value = oldVal;
                }

                cbDogovor.Refresh();
            }
        }

        /// <summary>
        /// Процедура заполнения списка сотрудников, по которым была тарификация
        /// </summary>
        private void FillComboBoxEmployee()
        {
            string oldVal = cbEmployee.Value;
            cbEmployee.EmptyValueText = string.Format("- {0} -", TM_FDataEmptyEmployee);
            cbEmployee.Items.Clear();
            cbEmployee.DataItems.Clear();
            cbEmployee.Value = "";
            cbEmployee.KeyField = "КодСотрудника";
            cbEmployee.ValueField = "Сотрудник";
            Dictionary<string, object> sqlParams = GetSQLParams(2);
            DataTable dt = DBManager.GetData(SQL_Queries.SQL_СотрудникиПоКоторымБылаТарификация,
                                             Config.DS_accounting_phone, CommandType.Text, sqlParams);
            cbEmployee.FillList = dt.AsEnumerable;

            if (V4IsPostBack)
            {
                EnumerableRowCollection<DataRow> result = from r in dt.AsEnumerable()
                                                          where
                                                              oldVal.Length == 0 ||
                                                              (oldVal.Length > 0 &&
                                                               r.Field<int>("КодСотрудника") == int.Parse(oldVal))
                                                          select r;

                DataView dv = result.AsDataView();

                if (dv.Count > 0)
                {
                    if (oldVal.Length > 0)
                        cbEmployee.Value = oldVal;
                }
                


                cbEmployee.Refresh();
              
            }
        }

        /// <summary>
        /// Процедура заполнения список телефонных номеров, по которым была тарифакация
        /// </summary>
        private void FillComboBoxPhone()
        {
            string oldVal = cbPhone.Value;
            cbPhone.EmptyValueText = string.Format("- {0} -", TM_FDataEmptyPhone);
            cbPhone.Items.Clear();
            cbPhone.DataItems.Clear();
            cbPhone.Value = "";
            cbPhone.KeyField = "Абонент";
            cbPhone.ValueField = "Абонент";
            Dictionary<string, object> sqlParams = GetSQLParams(3);
            DataTable dt = DBManager.GetData(SQL_Queries.SQL_ТелефоныПоКоторымБылаТарификация,
                                             Config.DS_accounting_phone, CommandType.Text, sqlParams);
            cbPhone.FillList = dt.AsEnumerable;

            if (V4IsPostBack)
            {
                EnumerableRowCollection<DataRow> result = from r in dt.AsEnumerable()
                                                          where
                                                              oldVal.Length == 0 ||
                                                              (oldVal.Length > 0 && r.Field<string>("Абонент") == oldVal)
                                                          select r;

                DataView dv = result.AsDataView();

                if (dv.Count > 0)
                {
                    if (oldVal.Length > 0)
                        cbPhone.Value = oldVal;
                    //else if (dv.Count == 1)
                    //    cbPhone.Value = dv[0]["Абонент"].ToString();
                }
                //else
                //{
                //    if (dt.Rows.Count == 1)
                //        cbPhone.Value = dt.Rows[0]["Абонент"].ToString();
                //}
                cbPhone.Refresh();
            }
        }

        #endregion


        #region Filter

        /// <summary>
        /// Процедура поиска данных, согласно устновленному фильтру
        /// </summary>
        private void FilterApply()
        {
            Dictionary<string, object> sqlParams = GetSQLParams(-1);
            DataTable dt = DBManager.GetData(SQL_Queries.SQL_ТарификацияИтоги, Config.DS_accounting_phone,
                                             CommandType.Text, sqlParams);

            if (dt.Rows.Count == 0)
            {
                CurrentLevel = "1";
                JS.Write(
                    "var objDR = document.getElementById('divResult'); currentLevel={0}; if (objDR) objDR.innerHTML='';",
                    CurrentLevel);
                JS.Write("var objLV = document.getElementById('divLevel'); if (objLV) objLV.style.display='none';");
                JS.Write("var objLG = document.getElementById('divLegenda'); if (objLG) objLG.style.display='none';");
                JS.Write("var objEmpty = document.getElementById('divEmpty'); if (objEmpty) objEmpty.style.display='block';");
                RestoreCursor();
                return;
            }
            JS.Write("var objLV = document.getElementById('divLevel'); if (objLV) objLV.style.display='block';");
            JS.Write("var objLG = document.getElementById('divLegenda'); if (objLG) objLG.style.display='block';");
            JS.Write("var objEmpty = document.getElementById('divEmpty'); if (objEmpty) objEmpty.style.display='none';");
            RenderTableResult(dt);
            RestoreCursor();
        }

        /// <summary>
        /// Процедура очистки установленного фильтра и таблицы с данными
        /// </summary>
        private void FilterClear()
        {
            chOpenMonth.Value = "0";
            DisplayRowsData(chOpenMonth.Value);
            
            if (cbYear.Contains(DateTime.Today.Year.ToString()))
                cbYear.Value = DateTime.Today.Year.ToString();
            else
            {
                 if (cbYear.Items.Count > 1)
                 {
                     var lastYear = cbYear.Items.Values.Last();
                     if (lastYear != null)
                         cbYear.Value = lastYear.ToString();
                 }
            }

            cbMonth.Value = "";
            cbDogovor.Value = "";
            cbEmployee.Value = "";
            cbPhone.Value = "";

            if (cbYear.Value.Length > 0 && cbYear.Value == DateTime.Today.Year.ToString())
            {
                if (DateTime.Today.Month == 1)
                {
                    cbMonth.Value = "12";
                    cbYear.Value = (DateTime.Today.Year - 1).ToString();
                }
                else
                    cbMonth.Value = (DateTime.Today.Month - 1).ToString();
            }

            FillComboBoxDogovor();
            FillComboBoxEmployee();
            FillComboBoxPhone();

            ResultClear();

        }

        /// <summary>
        /// Процедура очистки таблицы с данными результатов фильтрации
        /// </summary>
        private void ResultClear()
        {
            JS.Write("var objLV = document.getElementById('divLevel'); if (objLV) objLV.style.display='none';");
            JS.Write("var objDR = document.getElementById('divResult'); if (objDR) objDR.innerHTML='';");
            JS.Write("var objLG = document.getElementById('divLegenda'); if (objLG) objLG.style.display='none';");
            JS.Write("var objEmpty = document.getElementById('divEmpty'); if (objEmpty) objEmpty.style.display='none';");
            RestoreCursor();
        }

        /// <summary>
        /// Функция, возвращающая словарь с параметрами, установленного фильтра
        /// </summary>
        /// <param name="inx">Индекс параметра, соответсвующий порядку контролов фильтрации</param>
        /// <returns>Словарь с SQL-параметрами</returns>
        private Dictionary<string, object> GetSQLParams(int inx)
        {
            var sqlParams = new Dictionary<string, object>();

            int? year = null;
            int? month = null;
            int? dogovor = null;
            int? employee = null;

            int y;
 
            if (inx != 0) sqlParams.Add("@OpenMonth", chOpenMonth.Value.Equals("1") ? 1 : 0);
            if (inx != 0)
            {
                if (cbYear.Value.Length == 0)
                    sqlParams.Add("@Год", year);
                else if (!Int32.TryParse(cbYear.Value, out y))
                    sqlParams.Add("@Год", -1);
                else
                    sqlParams.Add("@Год", int.Parse(cbYear.Value));
            }
            if (inx != 0) sqlParams.Add("@Месяц", cbMonth.Value.Length == 0 ? month : int.Parse(cbMonth.Value));
            if (inx != 1)
                sqlParams.Add("@КодДоговора", cbDogovor.Value.Length == 0 ? dogovor : int.Parse(cbDogovor.Value));
            if (inx != 2)
                sqlParams.Add("@КодСотрудника", cbEmployee.Value.Length == 0 ? employee : int.Parse(cbEmployee.Value));

            if (inx != 3) sqlParams.Add("@Абонент", cbPhone.Value);


            return sqlParams;
        }

        #endregion


        #region Render

        /// <summary>
        /// Процедура, отвечающая за вывод найденных данных
        /// </summary>
        /// <param name="dt">Истояник данных</param>
        private void RenderTableResult(DataTable dt)
        {
            var sb = new StringBuilder();
            var sbBuff = new StringBuilder();
            var dtNDS = new DataTable();

            dtNDS.Columns.Add(new DataColumn("КодВалюты", typeof (int)));
            dtNDS.Columns.Add(new DataColumn("КодДоговора", typeof (int)));
            dtNDS.Columns.Add(new DataColumn("НДС", typeof (decimal)));
            dtNDS.Columns.Add(new DataColumn("НДССотрудника", typeof (decimal)));

            sbBuff.Append(@"<table>");

            RenderTableHeader(sbBuff);

            if (cbMonth.Value.Length > 0 || chOpenMonth.Value.Equals("1"))
                RenderTableItemsLevelFirst(dt, dtNDS, sb);
            else
                RenderTableItemsAllMonthsLevelFirst(dt, dtNDS, sb);
                

            RenderTableHeaderItogo(dt, dtNDS, sbBuff);

            sbBuff.Append(sb.ToString());
            sbBuff.Append("</table>");

            RefreshTableResult(sbBuff);
        }

        /// <summary>
        /// Процедура, отвечающая за вывод заголовка таблицы с данными
        /// </summary>
        /// <param name="sb">HTML-контент</param>
        private void RenderTableHeader(StringBuilder sb)
        {
            sb.Append("<tr class='gridHeader'>");

            sb.AppendFormat("<td colspan='5'>{0}</td>", (cbMonth.Value.Length > 0 || chOpenMonth.Value.Equals("1"))? HttpUtility.HtmlEncode(TM_FDogovor):HttpUtility.HtmlEncode(TM_FMonth));
            sb.AppendFormat("<td>{0}</td>", HttpUtility.HtmlEncode(TM_TblColumn2));
            sb.AppendFormat("<td>{0}</td>", HttpUtility.HtmlEncode(TM_TblColumn3));
            sb.AppendFormat("<td>{0}<br>{1}</td>", HttpUtility.HtmlEncode(TM_TblColumn4_0), HttpUtility.HtmlEncode(TM_TblColumn4_1));

            sb.Append("</tr>");
        }

        /// <summary>
        /// Процедура, отвечающая за вывод итогов сумм по-валютно
        /// </summary>
        /// <param name="dt">Источник данных</param>
        /// <param name="dtNDS">Источник данных с насчитанными сумма НДС в разрезе валют</param>
        /// <param name="sb">HTML-контент</param>
        private void RenderTableHeaderItogo(DataTable dt, DataTable dtNDS, StringBuilder sb)
        {
            var results = from r in dt.AsEnumerable()
                          group r by new {КодВалюты = r.Field<int>("КодВалюты")}
                          into groupDogovor
                          orderby groupDogovor.Key.КодВалюты
                          select new
                                     {
                                         groupDogovor.Key.КодВалюты,
                                         Секунд = groupDogovor.Sum(r => r.Field<int>("Секунд")),
                                         Сумма = groupDogovor.Sum(r => r.Field<decimal>("Сумма")),
                                         СуммаСотрудника = groupDogovor.Sum(r => r.Field<decimal>("СуммаСотрудника"))
                                     };

            var resultsNDS = from r in dtNDS.AsEnumerable()
                             group r by new {КодВалюты = r.Field<int>("КодВалюты")}
                             into groupCurrency
                             orderby groupCurrency.Key.КодВалюты
                             select new
                                        {
                                            groupCurrency.Key.КодВалюты,
                                            НДС = groupCurrency.Sum(r => r.Field<decimal>("НДС")),
                                            НДССотрудника = groupCurrency.Sum(r => r.Field<decimal>("НДССотрудника"))
                                        };

            string currency = "";
            decimal sum = 0M;
            decimal sumE = 0M;
            foreach (var d in results)
            {
                DataRow currencyRow = (from m in _currencies.AsEnumerable()
                                       where m.Field<int>("КодВалюты") == d.КодВалюты
                                       select m).FirstOrDefault();
                if (currencyRow != null) currency = currencyRow["Валюта"].ToString();
                else currency = "";

                sum = d.Сумма;
                sumE = d.СуммаСотрудника;

                foreach (var n in resultsNDS)
                {
                    if (!d.КодВалюты.Equals(n.КодВалюты)) continue;
                    sum += n.НДС;
                    sumE += n.НДССотрудника;
                }

                sb.Append("<tr class='gridHeader'>");

                sb.Append("<td colspan='5'>&nbsp;</td>");

                sb.AppendFormat("<td style='text-align:right;'>{0}</td>", Convert.Second2TimeFormat(d.Секунд));
                sb.AppendFormat("<td style='text-align:right;'>{0}{1}</td>", sum.ToString("N2"), currency);
                sb.AppendFormat("<td style='text-align:right;'>{0}{1}</td>", sumE.ToString("N2"), currency);

                sb.Append("</tr>");
            }
        }

        /// <summary>
        /// Установление стиля колонок сумма и сумма, оплачиваемая сотрудником
        /// </summary>
        /// <param name="НДСНаСумму">Насчитывать НДС на общую сумму</param>
        /// <param name="НДСВТарификации">НДС уже включено в тарификацию</param>
        /// <param name="color">Цвет фона</param>
        /// <param name="title">Tooltip ячейки с суммой</param>
        private void SumTDStyleSet(byte? НДСНаСумму, byte? НДСВТарификации, ref СalculationNDS ndsCalc)
        {
            //Нет условий договора, не известно как считать НДС
            if (НДСНаСумму == null || НДСВТарификации == null)
            {
                ndsCalc = СalculationNDS.Unknown;
                return;
            }

            if (НДСВТарификации != null && НДСВТарификации.Value.Equals(1))
            {
                ndsCalc = СalculationNDS.NotRequired;
                return;
            }

            if (НДСНаСумму != null && НДСНаСумму.Value.Equals(1))
            {
                ndsCalc = СalculationNDS.OnTotalSumOfContract;
                return;
            }

            if (НДСНаСумму != null && НДСНаСумму.Value.Equals(0) && НДСВТарификации != null &&
                НДСВТарификации.Value.Equals(0))
            {
                ndsCalc = СalculationNDS.OnCostOfEachCall;
                return;
            }
            ndsCalc = СalculationNDS.Unknown;
        }

        /// <summary>
        /// Процедура вывода первого уровня данных - группировка по договора
        /// </summary>
        /// <param name="dt">Источник данных</param>
        /// <param name="dtNDS">Источник данных, в который буден насчитан НДС</param>
        /// <param name="sb">HTML-контент</param>
        private void RenderTableItemsLevelFirst(DataTable dt, DataTable dtNDS, StringBuilder sb)
        {
            var results = from r in dt.AsEnumerable()
                          group r by new
                                         {
                                             КодДоговора = r.Field<int>("КодДоговора"),
                                             КодВалюты = r.Field<int>("КодВалюты"),
                                             Договор = r.Field<string>("Договор"),
                                             ОкруглениеСуммы = r.Field<byte?>("ОкруглениеСуммы"),
                                             СтавкаНДС = r.Field<decimal?>("СтавкаНДС"),
                                             НДСВТарификации = r.Field<byte?>("НДСВТарификации"),
                                             НДСНаСумму = r.Field<byte?>("НДСНаСумму")
                                         }
                          into groupDogovor
                          orderby groupDogovor.Key.Договор
                          select new
                                     {
                                         groupDogovor.Key.КодДоговора,
                                         groupDogovor.Key.КодВалюты,
                                         groupDogovor.Key.Договор,
                                         groupDogovor.Key.ОкруглениеСуммы,
                                         groupDogovor.Key.СтавкаНДС,
                                         groupDogovor.Key.НДСВТарификации,
                                         groupDogovor.Key.НДСНаСумму,
                                         Секунд = groupDogovor.Sum(r => r.Field<int>("Секунд")),
                                         Сумма = groupDogovor.Sum(r => r.Field<decimal>("Сумма")),
                                         СуммаСотрудника = groupDogovor.Sum(r => r.Field<decimal>("СуммаСотрудника"))
                                     };

            string currency = "";
            decimal sum = 0M;
            decimal sumE = 0M;
            string color = "";
            string title = "";
            СalculationNDS ndsCalc = СalculationNDS.Unknown;
            StringBuilder sbNextLevel = new StringBuilder();
            DataRow rowNDS;
            
            foreach (var d in results)
            {
                DataRow currencyRow = (from m in _currencies.AsEnumerable()
                                       where m.Field<int>("КодВалюты") == d.КодВалюты
                                       select m).FirstOrDefault();
                if (currencyRow != null) currency = currencyRow["Валюта"].ToString();
                else currency = "";

                SumTDStyleSet(d.НДСНаСумму, d.НДСВТарификации, ref ndsCalc);

                color = "background:{0};";
                title = "title='{0}'";

                RenderTableItemsLevelTwo(dt, d.КодДоговора, d.Договор, currency, ndsCalc, d.СтавкаНДС,
                                         d.НДСВТарификации, d.ОкруглениеСуммы,
                                         "tr1_" + d.КодДоговора, sbNextLevel, ref sum, ref sumE);

                if (ndsCalc.Equals(СalculationNDS.OnTotalSumOfContract))
                {
                    sum = d.Сумма*(1 + d.СтавкаНДС.Value);
                    sumE = d.СуммаСотрудника*(1 + d.СтавкаНДС.Value);
                }
                else if (!ndsCalc.Equals(СalculationNDS.OnCostOfEachCall))
                {
                    sum = d.Сумма;
                    sumE = d.СуммаСотрудника;
                }
                
                if (sum > d.Сумма || sumE > d.СуммаСотрудника)
                {
                    rowNDS = dtNDS.NewRow();
                    rowNDS["КодВалюты"] = d.КодВалюты;
                    rowNDS["КодДоговора"] = d.КодДоговора;
                    rowNDS["НДС"] = sum - d.Сумма;
                    rowNDS["НДССотрудника"] = sumE - d.СуммаСотрудника;
                    dtNDS.Rows.Add(rowNDS);
                }

                if (!(ndsCalc.Equals(СalculationNDS.Unknown)))
                {
                    color = string.Format(color, _colorWhite);
                    title = string.Format(title, TitleWhite);
                }
                else
                {
                    color = string.Format(color, _colorOrange);
                    title = string.Format(title, TitleOrange);
                }

                sb.AppendFormat(@"<tr class=""level"" level=1 id=""tr1_{0}"">", d.КодДоговора);
                sb.AppendFormat("<td style='width:20px'>{0}</td>",
                                string.Format(
                                    "<img class=\"levelImg btn\" onclick=\"displayLevelItem('tr1_{0}');\" src='/styles/plus.gif'>",
                                    d.КодДоговора));
                sb.AppendFormat("<td colspan=4 noWrap>{0}</td>", d.Договор);
                sb.AppendFormat("<td style='text-align:right;'>{0}</td>", Convert.Second2TimeFormat(d.Секунд));
                sb.AppendFormat("<td style='text-align:right;{2}' {3}>{0}{1}</td>",
                                sum.ToString("N2"),
                                currency,
                                color,
                                title);

                sb.AppendFormat("<td style='text-align:right;{2}' {3}>{0}{1}</td>",
                                sumE.ToString("N2"),
                                currency,
                                color,
                                title);
                sb.Append("</tr>");

                sb.Append(sbNextLevel.ToString());

                sbNextLevel.Remove(0, sbNextLevel.Length);
                sum = 0M;
                sumE = 0M;
                color = "";
                title = "";
            }
        }


       

        /// <summary>
        /// Процедура вывода второго уровня данных - группировка по сотруднику
        /// </summary>
        /// <param name="dt">Источник данных</param>
        /// <param name="d">КодДоговора</param>
        /// <param name="dText">Название договора</param>
        /// <param name="currency">Валюта</param>
        /// <param name="ndsAllSumm">Насчитывать НДС на всю сумму</param>
        /// <param name="ndsStavka">Ставка НДС в формате 0.18</param>
        /// <param name="ndsIn">НДС включен в тарификацию</param>
        /// <param name="scale">Точность округления</param>
        /// <param name="color">Цвет фона ячеек с суммами</param>
        /// <param name="title">Tooltip ячеек с суммами</param>
        /// <param name="trId">Идентификатор ряда в таблице</param>
        /// <param name="sb">HTML-контент</param>
        private void RenderTableItemsLevelTwo(DataTable dt, int d, string dText, string currency, СalculationNDS ndsCalc,
                                               decimal? ndsStavka, int? ndsIn, int? scale,
                                               string trId, StringBuilder sb, ref decimal calcSum, ref decimal calcSumE)
        {
            var results = from r in dt.AsEnumerable()
                          where r.Field<int>("КодДоговора") == d
                          group r by
                              new
                                  {
                                      КодСотрудника = r.Field<int>("КодСотрудника"),
                                      Сотрудник = r.Field<string>("Сотрудник")
                                  }
                          into groupEmployee
                          orderby groupEmployee.Key.Сотрудник
                          select new
                                     {
                                         groupEmployee.Key.КодСотрудника,
                                         groupEmployee.Key.Сотрудник,
                                         Секунд = groupEmployee.Sum(r => r.Field<int>("Секунд")),
                                         Сумма = groupEmployee.Sum(r => r.Field<decimal>("Сумма")),
                                         СуммаСотрудника = groupEmployee.Sum(r => r.Field<decimal>("СуммаСотрудника"))
                                     };

            decimal sum = 0M;
            decimal sumE = 0M;
            StringBuilder sbNextLevel = new StringBuilder();
            string color = "background:{0};";
            string title = "title='{0}'";

            if (ndsCalc.Equals(СalculationNDS.Unknown))
            {
                color = string.Format(color, _colorOrange);
                title = string.Format(title, TitleOrange);
            }
            else if (!ndsCalc.Equals(СalculationNDS.OnTotalSumOfContract))
            {
                color = string.Format(color, _colorWhite);
                title = string.Format(title, TitleWhite);
            }
            else
            {
                color = string.Format(color, _colorGray);
                title = string.Format(title, TitleGray);
            }
           

            foreach (var e in results)
            {
                RenderTableItemsLevelThree(dt, d, dText, e.КодСотрудника, currency, ndsCalc, ndsStavka, ndsIn, scale,
                                            color, title, "tr2_" + d + "_" + e.КодСотрудника, sbNextLevel, ref sum, ref sumE);

                
                calcSum += sum;
                calcSumE += sumE;

                sb.AppendFormat(@"<tr class=""level"" level=2 id=""tr2_{0}"" name=""{1}"" style=""display:none;"">",
                                d + "_" + e.КодСотрудника, trId);
                sb.AppendFormat("<td style='width:20px'>&nbsp;</td>");
                sb.AppendFormat("<td style='width:20px'>{0}</td>",
                                string.Format(
                                    "<img class=\"levelImg  btn\" onclick=\"displayLevelItem('tr2_{0}');\" src='/styles/plus.gif'>",
                                    d + "_" + e.КодСотрудника));
                
                sb.AppendFormat("<td colspan=3 {1}>{0}</td>", e.Сотрудник, e.КодСотрудника < 1? "style='color:gray;'":"");
                sb.AppendFormat("<td style='text-align:right;'>{0}</td>", Convert.Second2TimeFormat(e.Секунд));
                sb.AppendFormat("<td style='text-align:right;{2}' {3}>{0}{1}</td>", sum.ToString("N2"), currency, color,
                                title);
                sb.AppendFormat("<td style='text-align:right;{2}' {3}>{0}{1}</td>", sumE.ToString("N2"), currency, color,
                                title);
                sb.Append("</tr>");

                sb.Append(sbNextLevel.ToString());

                sbNextLevel.Remove(0, sbNextLevel.Length);
                sum = 0M;
                sumE = 0M;

            }
        }

 

        /// <summary>
        /// Процедура вывода третьего уровня данных - группировка по абоненту
        /// </summary>
        /// <param name="dt">Источник данны</param>
        /// <param name="d">КодДоговора</param>
        /// <param name="dText">Название договора</param>
        /// <param name="p">КодСотрудника</param>
        /// <param name="currency">Валюта</param>
        /// <param name="ndsAllSumm">Насчитывать НДС на всю сумму</param>
        /// <param name="ndsStavka">Ставка НДС в формате 0.18</param>
        /// <param name="ndsIn">НДС включен в тарификацию</param>
        /// <param name="scale">Точность округления</param>
        /// <param name="color">Цвет фона ячеек с суммами</param>
        /// <param name="title">Всплывающее описание цвета</param>
        /// <param name="trId">Идентификатор ряда в таблице</param>
        /// <param name="sb">HTML-контент</param>
        private void RenderTableItemsLevelThree(DataTable dt, int d, string dText, int p, string currency,
                                                 СalculationNDS ndsCalc, decimal? ndsStavka, int? ndsIn, int? scale,
                                                 string color, string title, string trId, StringBuilder sb, ref decimal calcSum, ref decimal calcSumE)
        {
            var results = from r in dt.AsEnumerable()
                          where r.Field<int>("КодДоговора") == d && r.Field<int>("КодСотрудника") == p
                          group r by
                              new
                                  {
                                      Абонент = r.Field<string>("Абонент"),
                                      Подчинённый = r.Field<int>("Подчинённый")
                                  }
                          into groupPhone
                          orderby groupPhone.Key.Абонент
                          select new
                                     {
                                         groupPhone.Key.Абонент,
                                         groupPhone.Key.Подчинённый,
                                         Секунд = groupPhone.Sum(r => r.Field<int>("Секунд")),
                                         Сумма = groupPhone.Sum(r => r.Field<decimal>("Сумма")),
                                         СуммаСотрудника = groupPhone.Sum(r => r.Field<decimal>("СуммаСотрудника"))
                                     };
            string pOpenMonth = chOpenMonth.Value.Length == 0 ? "0" : chOpenMonth.Value;
            string pYear = pOpenMonth.Equals("1") ? "0" : cbYear.Value;
            string pMonth = pOpenMonth.Equals("1") ? "0" : cbMonth.Value;
            string pDogovor = d.ToString();
            string pDogovorT = HttpUtility.HtmlEncode(dText);
            string pUser = p.ToString();
            string pPhone = "";

            decimal sum = 0M;
            decimal sumE = 0M;

            foreach (var ph in results)
            {
                pPhone = ph.Абонент;

                sum = ndsCalc.Equals(СalculationNDS.OnCostOfEachCall) ? ph.Сумма * (1 + ndsStavka.Value) : ph.Сумма;
                sumE = ndsCalc.Equals(СalculationNDS.OnCostOfEachCall) ? ph.СуммаСотрудника * (1 + ndsStavka.Value) : ph.СуммаСотрудника;

                calcSum += sum;
                calcSumE += sumE;

                sb.AppendFormat(@"<tr class=""level level3"" level=3  name=""{0}"" style=""display:none; "">", trId);
                sb.AppendFormat("<td style='width:20px'>&nbsp;</td>");
                sb.AppendFormat("<td style='width:20px'>&nbsp;</td>");
                sb.AppendFormat("<td style='width:20px; text-align:right;'>{0}</td>", ph.Подчинённый.Equals(1) && p > 0
                                                                                          ? string.Format(
                                                                                              "<img class=\"btn\" src='/styles/detail.gif' border='0' onclick=\"openDetailItem('{0}','{1}','{2}','{3}','{4}','{5}','{6}', '0', '{7}','{8}', '{9}', '{10}', '{11}', '{12}');\" title='{13}' />"
                                                                                              , pOpenMonth
                                                                                              , pYear
                                                                                              , pMonth
                                                                                              , pDogovor
                                                                                              ,
                                                                                              HttpUtility.
                                                                                                  JavaScriptStringEncode
                                                                                                  (pDogovorT)
                                                                                              , HttpUtility.
                                                                                                  JavaScriptStringEncode
                                                                                                  (pUser)
                                                                                              , HttpUtility.
                                                                                                  JavaScriptStringEncode
                                                                                                  (pPhone)
                                                                                              , System.Convert.ToInt32(ndsCalc) 
                                                                                              ,
                                                                                              !ndsStavka.HasValue
                                                                                                  ? 1
                                                                                                  : ndsStavka.Value
                                                                                              ,
                                                                                              !ndsIn.HasValue
                                                                                                  ? -1
                                                                                                  : ndsIn.Value
                                                                                              ,
                                                                                              !scale.HasValue
                                                                                                  ? 2
                                                                                                  : scale.Value
                                                                                              ,
                                                                                              HttpUtility.
                                                                                                  JavaScriptStringEncode
                                                                                                  (color)
                                                                                              ,
                                                                                              HttpUtility.
                                                                                                  JavaScriptStringEncode
                                                                                                  (title)
                                                                                              , HttpUtility.
                                                                                                  JavaScriptStringEncode
                                                                                                  (TM_RptDetail))
                                                                                          : "&nbsp;");
                sb.AppendFormat(@"<td colspan=2><span {1}>{0}</span></td>", ph.Абонент,
                    ph.Подчинённый.Equals(1) && p > 0
                        ? string.Format(@"data=""{0}"" name=""phone""", p + "_" + ph.Абонент)
                        : "");
                sb.AppendFormat("<td style='text-align:right;'>{0}</td>", Convert.Second2TimeFormat(ph.Секунд));
                sb.AppendFormat("<td style='text-align:right;{2}' {3}>{0}{1}</td>", sum.ToString("N2"), currency, color,
                                title);
                sb.AppendFormat("<td style='text-align:right;{2}' {3}>{0}{1}</td>", sumE.ToString("N2"), currency, color,
                                title);

                sb.Append("</tr>");
            }
        }

     
        /// <summary>
        /// Процедура вывода первого уровня данных - группировка по месяцу
        /// </summary>
        /// <param name="dt">Источник данных</param>
        /// <param name="dtNDS">Источник данных, в который буден насчитан НДС</param>
        /// <param name="sb">HTML-контент</param>
        private void RenderTableItemsAllMonthsLevelFirst(DataTable dt, DataTable dtNDS, StringBuilder sb)
        {
            var dtSum = new DataTable("MonthSum");
            DataRow rowSum;
            decimal sum = 0M;
            decimal sumE = 0M;
            int currencyId = 0;
            int sec = 0;
            СalculationNDS ndsCalc = СalculationNDS.Unknown;
            StringBuilder sbNextLevel = new StringBuilder();
            

            var resultSum = from r in dt.AsEnumerable()
                            group r by new
                            {
                                Месяц = r.Field<int>("Месяц"),
                                КодВалюты = r.Field<int>("КодВалюты"),
                                ОкруглениеСуммы = r.Field<byte?>("ОкруглениеСуммы"),
                                СтавкаНДС = r.Field<decimal?>("СтавкаНДС"),
                                НДСВТарификации = r.Field<byte?>("НДСВТарификации"),
                                НДСНаСумму = r.Field<byte?>("НДСНаСумму")
                            }
                                into groupMonth
                                orderby groupMonth.Key.Месяц
                                select new
                                {
                                    groupMonth.Key.Месяц,
                                    groupMonth.Key.КодВалюты,
                                    groupMonth.Key.ОкруглениеСуммы,
                                    groupMonth.Key.СтавкаНДС,
                                    groupMonth.Key.НДСВТарификации,
                                    groupMonth.Key.НДСНаСумму,
                                    Секунд = groupMonth.Sum(rs => rs.Field<int>("Секунд")),
                                    Сумма = groupMonth.Sum(rs => rs.Field<decimal>("Сумма")),
                                    СуммаСотрудника = groupMonth.Sum(rs => rs.Field<decimal>("СуммаСотрудника"))
                                };


            dtSum.Columns.Add(new DataColumn("Месяц", typeof(int)));
            dtSum.Columns.Add(new DataColumn("КодВалюты", typeof(int)));
            dtSum.Columns.Add(new DataColumn("Секунд", typeof(int)));
            dtSum.Columns.Add(new DataColumn("Сумма", typeof(decimal)));
            dtSum.Columns.Add(new DataColumn("СуммаСотрудника", typeof(decimal)));

            foreach (var rs in resultSum)
            {
                sec = rs.Секунд;
                DataRow updateRow = dtSum.AsEnumerable().Where(r => r.Field<int>("Месяц") == rs.Месяц && r.Field<int>("КодВалюты") == rs.КодВалюты).SingleOrDefault();

                if (updateRow == null)
                {
                    rowSum = dtSum.NewRow();
                    rowSum["Месяц"] = rs.Месяц;
                    rowSum["КодВалюты"] = rs.КодВалюты;
                    rowSum["Секунд"] = sec;
                    rowSum["Сумма"] = 0;
                    rowSum["СуммаСотрудника"] = 0;
                    dtSum.Rows.Add(rowSum);
                }
                else
                {
                    updateRow.SetField("Секунд", (int)updateRow["Секунд"] + sec);
                   // updateRow.SetField("Сумма", (decimal)updateRow["Сумма"] + sum);
                   // updateRow.SetField("СуммаСотрудника", (decimal)updateRow["СуммаСотрудника"] + sumE);
                }

            }

            var months = from r in dtSum.AsEnumerable()
                         group r by new
                         {
                             Месяц = r.Field<int>("Месяц"),
                         }
                             into groupMonth
                             orderby groupMonth.Key.Месяц
                             select new
                             {
                                 groupMonth.Key.Месяц,
                             };

            CultureInfo culture = new CultureInfo(CurrentUser.Language);
            foreach (var m in months)
            {
                RenderTableItemsAllMonthsLevelTwo(dt, dtNDS, dtSum, m.Месяц, "tr1_" + m.Месяц, sbNextLevel);

                sb.AppendFormat(@"<tr class=""level"" level=1 id=""tr1_{0}"">", m.Месяц);
                sb.AppendFormat("<td style='width:20px'>{0}</td>",
                                string.Format(
                                    "<img class=\"levelImg btn\" onclick=\"displayLevelItem('tr1_{0}');\" src='/styles/plus.gif'>",
                                    m.Месяц));
                sb.AppendFormat("<td colspan=4 noWrap>{0}</td>", culture.DateTimeFormat.GetMonthName(m.Месяц));
                RenderItogoAllMonthsFirstLevel(m.Месяц, dtSum, sb);
                sb.Append("</tr>");

                sb.Append(sbNextLevel.ToString());

                sbNextLevel.Remove(0, sbNextLevel.Length);

                sum = 0M;
                sumE = 0M;
            }
        }
        /// <summary>
        /// Процедура вывода второго уровня данных - группировка по договору
        /// </summary>
        /// <param name="dt">Источник данных</param>
        /// <param name="dtNDS">Источник данных, в который буден насчитан НДС</param>
        /// <param name="m">Месяц, за который необходимо показать договора</param>
        /// <param name="trId">Идентификатор первого уровня</param>
        /// <param name="sb">HTML-контент</param>
        private void RenderTableItemsAllMonthsLevelTwo(DataTable dt, DataTable dtNDS, DataTable dtSum, int m, string trId, StringBuilder sb)
        {
            var results = from r in dt.AsEnumerable()
                          where r.Field<int>("Месяц") == m
                          group r by new
                          {
                              Месяц = r.Field<int>("Месяц"),
                              КодДоговора = r.Field<int>("КодДоговора"),
                              КодВалюты = r.Field<int>("КодВалюты"),
                              Договор = r.Field<string>("Договор"),
                              ОкруглениеСуммы = r.Field<byte?>("ОкруглениеСуммы"),
                              СтавкаНДС = r.Field<decimal?>("СтавкаНДС"),
                              НДСВТарификации = r.Field<byte?>("НДСВТарификации"),
                              НДСНаСумму = r.Field<byte?>("НДСНаСумму")
                          }
                              into groupDogovor
                              orderby groupDogovor.Key.Договор
                              select new
                              {
                                  groupDogovor.Key.Месяц,
                                  groupDogovor.Key.КодДоговора,
                                  groupDogovor.Key.КодВалюты,
                                  groupDogovor.Key.Договор,
                                  groupDogovor.Key.ОкруглениеСуммы,
                                  groupDogovor.Key.СтавкаНДС,
                                  groupDogovor.Key.НДСВТарификации,
                                  groupDogovor.Key.НДСНаСумму,
                                  Секунд = groupDogovor.Sum(r => r.Field<int>("Секунд")),
                                  Сумма = groupDogovor.Sum(r => r.Field<decimal>("Сумма")),
                                  СуммаСотрудника = groupDogovor.Sum(r => r.Field<decimal>("СуммаСотрудника"))
                              };

            string currency = "";
            decimal sum = 0M;
            decimal sumE = 0M;
            string color = "";
            string title = "";
           
            СalculationNDS calcNds = СalculationNDS.Unknown;
            StringBuilder sbNextLevel = new StringBuilder();
            DataRow rowNDS;

            foreach (var d in results)
            {
                DataRow currencyRow = (from cur in _currencies.AsEnumerable()
                                       where cur.Field<int>("КодВалюты") == d.КодВалюты
                                       select cur).FirstOrDefault();
                if (currencyRow != null) currency = currencyRow["Валюта"].ToString();
                else currency = "";

                SumTDStyleSet(d.НДСНаСумму, d.НДСВТарификации, ref calcNds);
                color = "background:{0};";
                title = "title='{0}'";

                RenderTableItemsAllMonthsLevelThree(dt, d.КодДоговора, m, currency, calcNds, d.СтавкаНДС,
                          d.НДСВТарификации, d.ОкруглениеСуммы, 
                           "tr2_" + m + "_" + d.КодДоговора, sbNextLevel, ref sum, ref sumE);

                if (calcNds.Equals(СalculationNDS.OnTotalSumOfContract))
                {
                    sum = d.Сумма * (1 + d.СтавкаНДС.Value);
                    sumE = d.СуммаСотрудника * (1 + d.СтавкаНДС.Value);
                }
                else if (!calcNds.Equals(СalculationNDS.OnCostOfEachCall))
                {
                    sum = d.Сумма;
                    sumE = d.СуммаСотрудника;
                }

                DataRow updateRow = dtSum.AsEnumerable().Where(r => r.Field<int>("Месяц") == d.Месяц && r.Field<int>("КодВалюты") == d.КодВалюты).SingleOrDefault();
                if (updateRow != null)
                {   
                    updateRow.SetField("Сумма", (decimal)updateRow["Сумма"] + sum);
                    updateRow.SetField("СуммаСотрудника", (decimal)updateRow["СуммаСотрудника"] + sumE);
                }
              
                if (sum > d.Сумма || sumE > d.СуммаСотрудника)
                {
                    rowNDS = dtNDS.NewRow();
                    rowNDS["КодВалюты"] = d.КодВалюты;
                    rowNDS["КодДоговора"] = d.КодДоговора;
                    rowNDS["НДС"] = sum - d.Сумма;
                    rowNDS["НДССотрудника"] = sumE - d.СуммаСотрудника;
                    dtNDS.Rows.Add(rowNDS);
                }

                if (!(calcNds.Equals(СalculationNDS.Unknown)))
                {
                    color = string.Format(color, _colorWhite);
                    title = string.Format(title, TitleWhite);
                }
                else
                {
                    color = string.Format(color, _colorOrange);
                    title = string.Format(title, TitleOrange);
                }

                sb.AppendFormat(@"<tr class=""level"" level=2 id=""tr2_{0}"" name=""{1}"" style=""display:none;"">",
                m + "_" + d.КодДоговора, trId);
                sb.AppendFormat("<td style='width:20px'>&nbsp;</td>");
                sb.AppendFormat("<td style='width:20px'>{0}</td>",
                                string.Format(
                                    "<img class=\"levelImg  btn\" onclick=\"displayLevelItem('tr2_{0}');\" src='/styles/plus.gif'>",
                                    m + "_" + d.КодДоговора));

                sb.AppendFormat("<td colspan=3 {1}>{0}</td>", d.Договор, d.КодДоговора < 1 ? "style='color:gray;'" : "");
                sb.AppendFormat("<td style='text-align:right;'>{0}</td>", Convert.Second2TimeFormat(d.Секунд));
                sb.AppendFormat("<td style='text-align:right;{2}' {3}>{0}{1}</td>", sum.ToString("N2"), currency, color,
                                title);
                sb.AppendFormat("<td style='text-align:right;{2}' {3}>{0}{1}</td>", sumE.ToString("N2"), currency, color,
                                title);
                sb.Append("</tr>");

                sb.Append(sbNextLevel.ToString());

                sbNextLevel.Remove(0, sbNextLevel.Length);
                color = "";
                title = "";
            }
        }
       
        /// <summary>
        /// Процедура вывода третьего уровня данных - группировка по сотруднику
        /// </summary>
        /// <param name="dt">Источник данных</param>
        /// <param name="d">>Договор, по которому необходимо показать сотрдуников</param>
        /// <param name="m">>Месяц, за который необходимо показать сотдуников</param>
        /// <param name="currency">Валюта договора</param>
        /// <param name="ndsAllSumm">Насчитывать НДС на всю сумму</param>
        /// <param name="ndsStavka">Ставка НДС в формате 0.18</param>
        /// <param name="ndsIn">НДС включен в тарификацию</param>
        /// <param name="scale">Точность округления</param>
        /// <param name="color">Цвет фона ячеек с суммами</param>
        /// <param name="title">Всплывающее описание цвета</param>
        /// <param name="trId">Идентификатор ряда второго уровня в таблице</param>
        /// <param name="sb">HTML-контент</param>
        private void RenderTableItemsAllMonthsLevelThree(DataTable dt, int d, int m, string currency, СalculationNDS calcNds,
                                               decimal? ndsStavka, int? ndsIn, int? scale, 
                                               string trId, StringBuilder sb, ref decimal calcSum, ref decimal calcSumE)
        {
            var results = from r in dt.AsEnumerable()
                          where r.Field<int>("КодДоговора") == d && r.Field<int>("Месяц") == m
                          group r by
                              new
                              {
                                  КодСотрудника = r.Field<int>("КодСотрудника"),
                                  Сотрудник = r.Field<string>("Сотрудник")
                              }
                              into groupEmployee
                              orderby groupEmployee.Key.Сотрудник
                              select new
                              {
                                  groupEmployee.Key.КодСотрудника,
                                  groupEmployee.Key.Сотрудник,
                                  Секунд = groupEmployee.Sum(r => r.Field<int>("Секунд")),
                                  Сумма = groupEmployee.Sum(r => r.Field<decimal>("Сумма")),
                                  СуммаСотрудника = groupEmployee.Sum(r => r.Field<decimal>("СуммаСотрудника"))
                              };

            decimal sum = 0M;
            decimal sumE = 0M;
            string color = "";
            string title = "";

            foreach (var u in results)
            {
                ItemsAllMonthsLevelFour(dt, d, m, u.КодСотрудника, calcNds, ndsStavka, ref sum, ref sumE);
                
                color = "background:{0};";
                title = "title='{0}'";
                calcSum += sum;
                calcSumE += sumE;

                if (calcNds.Equals(СalculationNDS.Unknown))
                {
                    color = string.Format(color, _colorOrange);
                    title = string.Format(title, TitleOrange);
                }
                else if (!calcNds.Equals(СalculationNDS.OnTotalSumOfContract))
                {
                    color = string.Format(color, _colorWhite);
                    title = string.Format(title, TitleWhite);
                }
                else
                {
                    color = string.Format(color, _colorGray);
                    title = string.Format(title, TitleGray);
                }
               
                sb.AppendFormat(@"<tr class=""level level3"" level=3  name=""{0}"" style=""display:none; "">", trId);
                sb.AppendFormat("<td style='width:20px'>&nbsp;</td>");
                sb.AppendFormat("<td style='width:20px'>&nbsp;</td>");
                sb.AppendFormat("<td style='width:20px; text-align:right;'>{0}</td>", "&nbsp;");
                sb.AppendFormat("<td colspan=2 {1}>{0}</td>", u.Сотрудник, u.КодСотрудника < 1 ? "style='color:gray;'" : "");
                sb.AppendFormat("<td style='text-align:right;'>{0}</td>", Convert.Second2TimeFormat(u.Секунд));
                sb.AppendFormat("<td style='text-align:right;{2}' {3}>{0}{1}</td>", sum.ToString("N2"), currency, color,
                                title);
                sb.AppendFormat("<td style='text-align:right;{2}' {3}>{0}{1}</td>", sumE.ToString("N2"), currency, color,
                                title);

                sb.Append("</tr>");

                sum = 0M;
                sumE = 0M;
            }
        }

        private void ItemsAllMonthsLevelFour(DataTable dt, int d, int m, int p, СalculationNDS calcNDS, decimal? ndsStavka,
            ref decimal calcSum, ref decimal calcSumE)
        {
            var results = from r in dt.AsEnumerable()
                          where r.Field<int>("КодДоговора") == d && r.Field<int>("Месяц") == m && r.Field<int>("КодСотрудника") == p
                group r by
                    new
                    {
                        Абонент = r.Field<string>("Абонент"),
                    }
                into groupPhone
                orderby groupPhone.Key.Абонент
                select new
                {
                    groupPhone.Key.Абонент,
                    Сумма = groupPhone.Sum(r => r.Field<decimal>("Сумма")),
                    СуммаСотрудника = groupPhone.Sum(r => r.Field<decimal>("СуммаСотрудника"))
                };

            foreach (var ph in results)
            {
                calcSum += calcNDS.Equals(СalculationNDS.OnCostOfEachCall) ? ph.Сумма*(1 + ndsStavka.Value) : ph.Сумма;
                calcSumE += calcNDS.Equals(СalculationNDS.OnCostOfEachCall)
                    ? ph.СуммаСотрудника*(1 + ndsStavka.Value)
                    : ph.СуммаСотрудника;
            }
        }


        /// <summary>
        /// Отображение списка сумм по-валютно в зависимости от указанного месяца
        /// </summary>
        /// <param name="month">Месяц</param>
        /// <param name="dtSum">Источник данных</param>
        /// <param name="sb">HTML-контент</param>
        private void RenderItogoAllMonthsFirstLevel(int month, DataTable dtSum, StringBuilder sb)
        {
            StringBuilder sbSec = new StringBuilder();
            StringBuilder sbSum = new StringBuilder();
            StringBuilder sbSumE = new StringBuilder();
            IEnumerable<DataRow> rows = dtSum.AsEnumerable().Where(r => r.Field<int>("Месяц") == month).OrderBy(r => r.Field<int>("КодВалюты"));
            string style = "style='BORDER-bottom:1px solid #efefef;'";
            string styleAlign = "style='text-align:right;' class='gridHeader'";

            string currency = "";
            int inx = 1;
            int cnt = rows.Count();
            foreach (DataRow r in rows)
            {
                DataRow currencyRow = (from m in _currencies.AsEnumerable()
                                       where m.Field<int>("КодВалюты") == r.Field<int>("КодВалюты")
                                       select m).FirstOrDefault();
                if (currencyRow != null) currency = currencyRow["Валюта"].ToString();
                else currency = "";

                sbSec.AppendFormat("<div {1}>{0}</div>", Convert.Second2TimeFormat(r.Field<int>("Секунд")),
                                    inx != cnt ? style : "");
                sbSum.AppendFormat("<div {2}>{0}{1}</div>", r.Field<decimal>("Сумма").ToString("N2"), currency,
                                    inx != cnt ? style : "");
                sbSumE.AppendFormat("<div {2}>{0}{1}</div>", r.Field<decimal>("СуммаСотрудника").ToString("N2"), currency,
                                    inx != cnt ? style : "");
                inx++;
            }

            sb.AppendFormat("<td {3}>{0}</td><td {3}>{1}</td><td {3}>{2}</td>",
                            sbSec.ToString(), sbSum.ToString(), sbSumE.ToString(), styleAlign);
        }

        
        
        /// <summary>
        /// Процедура отправки сформированного HTML-контента на клиента
        /// </summary>
        /// <param name="sb">HTML-контент</param>
        private void RefreshTableResult(StringBuilder sb)
        {
            JS.Write("var objDR = document.getElementById('divResult'); if (objDR) objDR.innerHTML='{0}';",
                     HttpUtility.JavaScriptStringEncode(sb.ToString()));
            JS.Write("displayLevel({0});", CurrentLevel);
            JS.Write("createTooltip('phone');");
            JS.Write("tooltipRegistry = {};");
        }

        /// <summary>
        /// Получение и рендеринг информации о Sim-карте
        /// </summary>
        /// <param name="id">Индентификатор контрола tooltip</param>
        /// <param name="data">Данные формате [КодСотрудника]_[НомерТелефона]</param>
        private void RenderSim(string id, string data)
        {
            string[] args = data.Split('_');
            Dictionary<string, object> sqlParams = new Dictionary<string, object>();
            DateTime startDate = new DateTime(int.Parse(cbYear.Value), int.Parse(cbMonth.Value), 1);
            DateTime endDate = new DateTime(int.Parse(cbYear.Value), int.Parse(cbMonth.Value),
                                    DateTime.DaysInMonth(int.Parse(cbYear.Value), int.Parse(cbMonth.Value)));

            sqlParams.Add("@StartDate", startDate);
            sqlParams.Add("@EndDate", endDate);
            sqlParams.Add("@CurrentMonth", int.Parse(chOpenMonth.Value));
            sqlParams.Add("@КодСотрудника", int.Parse(args[0]));
            sqlParams.Add("@НомерТелефона", args[1]);
            
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
                //sb.AppendFormat(@"<img src='/styles/sim.gif' border='0'><u style=""margin-bottom:5px;"">{0}</u>:<br>", TM_SimInfo);
                sb.AppendFormat(@"<img src='/styles/sim.gif' border='0'>");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i > 0)
                    {
                        sb.Append("<br/>");
                        sb.Append("<hr size=1/>");
                    }

                    dateFrom = dt.Rows[i]["От"].Equals(System.DBNull.Value) ? dateTo : (DateTime)dt.Rows[i]["От"];
                    dateTo = dt.Rows[i]["До"].Equals(System.DBNull.Value) ? dateTo : (DateTime) dt.Rows[i]["До"];

                    //if (!dateFrom.Equals(DateTime.MinValue) && dateFrom.Date >= startDate)
                    sb.AppendFormat("{1}: {0};", ((DateTime)dt.Rows[i]["От"]).ToString("dd.MM.yyyy"), TM_Issued);

                    if (!dateTo.Equals(DateTime.MinValue) && dateTo.Date <= endDate)
                        sb.AppendFormat("<br>{1}: {0}", dateTo.ToString("dd.MM.yyyy"), TM_Withdrawn);
                    
                    sb.AppendFormat("{0}",
                        dt.Rows[i]["Примечания"].ToString().Length > 0
                            ? "<br>" + dt.Rows[i]["Примечания"] 
                            : "");

                    render = dt.Rows[i]["Примечания"].ToString().Length > 0;
                    dateTo = DateTime.MinValue;
                }
            }

            //if (!render) sb.Remove(0, sb.Length);
            JS.Write("setTooltipValue('{0}','{1}','{2}');",
                     HttpUtility.JavaScriptStringEncode(id),
                     HttpUtility.JavaScriptStringEncode(data),
                     HttpUtility.JavaScriptStringEncode(sb.ToString()));
        }

        #endregion

       
        
    }
}