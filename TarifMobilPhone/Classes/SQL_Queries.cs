﻿using System.Data;

namespace Kesco.App.Web.TarifMobilPhone
{
    /// <summary>
    /// Класс, содержащий описание sql-запросов и методы работы с данными
    /// </summary>
    public class SQL_Queries
    {
        /// <summary>
        /// Поиск названия валюты по коду
        /// </summary>
        /// <param name="dt">Список валют</param>
        /// <param name="currencyId">Код валюты</param>
        /// <returns>Возвращает DataView c результатом поиска</returns>
        public static DataView FindCurrency(DataTable dt, int currencyId)
        {
            EnumerableRowCollection<DataRow> results = from myRow in dt.AsEnumerable()
                                                       where myRow.Field<int>("КодВалюты") == currencyId
                                                       select myRow;
            return results.AsDataView();
        }

        #region SQL_ПервыйГодТарификации

        /// <summary>
        /// Определение списка годов тарифкации, начиная с первого года тарификации до текущего
        /// </summary>
        public const string SQL_ПервыйГодТарификации =
            @"
DECLARE @Год int, @ТекущийГод int
DECLARE @Tbl TABLE(Год int PRIMARY KEY, ГодТекст nvarchar(20))
SELECT TOP 1 @Год = Год FROM vwТарификацияСотовыхИтоги WHERE Год > 0 ORDER BY Год

IF @Год IS NOT NULL
BEGIN
    SET @ТекущийГод = YEAR(GETUTCDATE())
    WHILE @Год <= @ТекущийГод
    BEGIN
        INSERT @Tbl SELECT @Год,  CONVERT(nvarchar,@Год)
        SET @Год = @Год + 1     
    END  
END
ELSE
BEGIN
    DECLARE @Lang char(2)
    SELECT @Lang = Язык FROM Инвентаризация.dbo.Сотрудники WHERE SID = SUSER_SID()
    INSERT @Tbl SELECT -1 Год, '- ' + CASE @Lang WHEN 'ru' THEN 'все' WHEN 'et' THEN 'kõik' ELSE 'all' END + ' -' ГодТекст
END

SELECT * FROM @Tbl ORDER BY Год
";

        #endregion

        #region SQL_ДоговораПоКоторымБылаТарификация

        /// <summary>
        /// Договора сотовой связи, по которым существует тарификация
        /// </summary>
        public const string SQL_ДоговораПоКоторымБылаТарификация =
            @"
SELECT КодДоговора, Договор 
FROM Инвентаризация.dbo.ДоговораСотовойСвязи Д (nolock) 
        WHERE EXISTS(
SELECT * FROM Тарификация.dbo.vwТарификацияСотовыхИтоги Т(nolock) 
WHERE Т.КодДоговора = Д.КодДоговора
AND ((@OpenMonth = 1 AND Год = 0 AND Месяц = 0)
			OR (@OpenMonth = 0 AND ((@Год IS NULL OR Год=@Год) AND (@Месяц IS NULL OR Месяц=@Месяц))))
) 
ORDER BY Договор";

        #endregion

        #region SQL_СотрудникиПоКоторымБылаТарификация

        /// <summary>
        /// Список сотрудников, по которым когда-либо была тарификация
        /// </summary>
        public const string SQL_СотрудникиПоКоторымБылаТарификация =
            @"
SELECT	CASE WHEN КодСотрудника IS NULL THEN '-1' ELSE КодСотрудника END КодСотрудника,  
	CASE WHEN КодСотрудника IS NULL THEN '- корректировки по договору -' WHEN КодСотрудника = 0 THEN '- не определён -' ELSE ФИО END Сотрудник 
FROM vwТарификацияСотовыхИтоги (nolock)
WHERE 	((@OpenMonth=1 AND Год=0 AND Месяц = 0)
			OR (@OpenMonth=0 AND ((@Год IS NULL OR Год=@Год) AND (@Месяц IS NULL OR Месяц=@Месяц))))	
	AND (@КодДоговора IS NULL OR КодДоговора=@КодДоговора)	
GROUP BY	
	CASE WHEN КодСотрудника IS NULL THEN '-1' ELSE КодСотрудника END,
	CASE WHEN КодСотрудника IS NULL THEN '- корректировки по договору -' WHEN КодСотрудника = 0 THEN '- не определён -' ELSE ФИО END
ORDER BY Сотрудник";

        #endregion

        #region SQL_ТелефоныПоКоторымБылаТарификация

        /// <summary>
        /// Список абонентских телефонных номеров, по которым когда-либо была тарификация
        /// </summary>
        public const string SQL_ТелефоныПоКоторымБылаТарификация =
            @"
SELECT Абонент
FROM vwТарификацияСотовыхИтоги
WHERE КодСотрудника IS NOT NULL
    AND ((@OpenMonth=1 AND Год=0 AND Месяц = 0)
			OR (@OpenMonth=0 AND ((@Год IS NULL OR Год=@Год) AND (@Месяц IS NULL OR Месяц=@Месяц))))	
	AND (@КодДоговора IS NULL OR КодДоговора = @КодДоговора)
	AND (@КодСотрудника IS NULL OR КодСотрудника = @КодСотрудника OR (@КодСотрудника = -1 AND КодСотрудника IS NULL) )	
GROUP BY Абонент
ORDER BY Абонент";

        #endregion

        #region SQL_ТарификацияИтоги

        /// <summary>
        /// Данные тарификации, сгрупированные по Год, Месяц, КодДоговора, КодСотрудника, Абонент
        /// </summary>
        public const string SQL_ТарификацияИтоги =
            @"
SELECT 	Т.КодДоговора,
        ISNULL(У.КодВалюты, -1) КодВалюты,
        У.ОкруглениеСуммы,
        У.СтавкаНДС,
        У.НДСВТарификации,
        У.НДСНаСумму,
        Д.Договор,
        CASE WHEN Т.КодСотрудника IS NULL THEN '-1' ELSE Т.КодСотрудника END КодСотрудника,
        CASE WHEN Т.КодСотрудника IS NULL THEN ' Корректировки по договору' WHEN Т.КодСотрудника = 0 THEN ' Сотрудник не определён' ELSE ФИО END Сотрудник, 		
        CASE WHEN Абонент = '' THEN ' - абонент неизвестен -' ELSE Абонент END AS Абонент,        
        SUM(Количество) Количество, 
        CONVERT(int, SUM(Секунд)) Секунд, 
        SUM(CASE WHEN @OpenMonth=1 THEN СуммаПредварительно ELSE Сумма END) Сумма,         
        SUM(CASE WHEN @OpenMonth=1 THEN СуммаСотрудникаПредварительно ELSE СуммаСотрудника END)  СуммаСотрудника,     
        --(SUM(Секунд) - 1) + (60 - (SUM(Секунд) - 1)%60) СекундРасчёт,  
        CASE WHEN Подчинённые.КодСотрудника IS NOT NULL THEN 1 
			 ELSE CASE WHEN Роли.КодРоли IS NOT NULL THEN 1 ELSE 0 END 
        END Подчинённый ,
        Год, 
        Месяц  		
FROM vwТарификацияСотовыхИтоги Т (nolock)
		INNER JOIN Инвентаризация.dbo.ДоговораСотовойСвязи  Д  (nolock) ON Т.КодДоговора = Д.КодДоговора		
        LEFT JOIN Инвентаризация.dbo.УсловияДоговоровСвязи  У (nolock)  ON Д.КодДокумента = У.КодДоговора
        LEFT JOIN (SELECT КодСотрудника FROM Инвентаризация.dbo.vwПодчинённые) Подчинённые ON Подчинённые.КодСотрудника = Т.КодСотрудника 
        LEFT JOIN (SELECT КодРоли from Инвентаризация.dbo.fn_ТекущиеРоли()) Роли ON Роли.КодРоли = 41
WHERE 
	((@OpenMonth=1 AND Т.Год=0 AND Т.Месяц = 0)
			OR (@OpenMonth=0 AND ((@Год IS NULL OR Т.Год=@Год) AND (@Месяц IS NULL OR Т.Месяц=@Месяц))))
    AND (@КодДоговора IS NULL OR Т.КодДоговора = @КодДоговора)		
    AND (@КодСотрудника IS NULL OR Т.КодСотрудника = @КодСотрудника OR (@КодСотрудника = -1 AND Т.КодСотрудника IS NULL))	
	AND (@Абонент = '' OR (@Абонент <> '' AND ISNULL(Т.Абонент,'') = @Абонент))
GROUP BY Т.КодДоговора, ISNULL(У.КодВалюты, -1), У.ОкруглениеСуммы, У.СтавкаНДС, У.НДСВТарификации, У.НДСНаСумму,Д.Договор,
    CASE WHEN Т.КодСотрудника IS NULL THEN '-1' ELSE Т.КодСотрудника END,
    CASE WHEN Т.КодСотрудника IS NULL THEN ' Корректировки по договору' WHEN Т.КодСотрудника = 0 THEN ' Сотрудник не определён' ELSE ФИО END,   
    CASE WHEN Абонент = '' THEN ' - абонент неизвестен -' ELSE Абонент END, Год, Месяц,
    CASE WHEN Подчинённые.КодСотрудника IS NOT NULL THEN 1 
	    ELSE CASE WHEN Роли.КодРоли IS NOT NULL THEN 1 ELSE 0 END 
    END";

        #endregion

        #region SQL_ТарификацияСотовых

        /// <summary>
        /// Детализация тарификации сотовых, ограниченная по договору, месяцу, году, сотруднику, абоненту
        /// в зависимости от указанных значений фильтра
        /// </summary>
        public const string SQL_ТарификацияСотовых =
            @"
SELECT * FROM 
    (SELECT TOP 100 PERCENT
            НачалоРазговора, Телефон, Абонент,
            CASE WHEN Исходящий = 1 THEN 'Исх.' ELSE 'Вх.' END Тип, 
            Услуга,
            LTRIM(ЗонаАбонента + ' ' + Направление) Описание,
            Секунд, Килобайт, 
            CASE Роуминг WHEN 1 THEN 'МГ' WHEN 2 THEN 'МН' ELSE '-' END Роуминг,
		    CASE WHEN @OpenMonth=1 THEN СуммаПредварительно ELSE Сумма END Сумма,
		    CASE WHEN @OpenMonth=1 THEN СуммаСотрудникаПредварительно ELSE СуммаСотрудника END СуммаСотрудника		
    FROM vwТарификацияСотовых Т (nolock)
    WHERE ((@OpenMonth = 1 AND Т.Год = 0 AND Т.Месяц = 0)
			    OR (@OpenMonth = 0 AND ((@Год IS NULL OR Т.Год = @Год) AND (@Месяц IS NULL OR Т.Месяц = @Месяц))))
	    AND (@КодДоговора IS NULL OR Т.КодДоговора = @КодДоговора)
        AND (ISNULL(@Договор,'') = '' OR  КодДоговора IN (SELECT КодДоговора FROM Инвентаризация.dbo.ДоговораСотовойСвязи WHERE Договор LIKE '%' + @Договор + '%'))
	    AND (@КодСотрудника IS NULL OR Т.КодСотрудника = @КодСотрудника OR (@КодСотрудника = -1 AND Т.КодСотрудника IS NULL))	
	    AND (ISNULL(@Абонент,'') = '' OR Т.Абонент=@Абонент)   
) Т0 
 {0}
";

        #endregion

        #region SQL_СписокВалют

        /// <summary>
        /// Список валют(данные получаются в зависимости от таблицы валюты)
        /// </summary>
        public const string SQL_СписокВалют =
            @"
SELECT 0 КодВалюты,'у.е.' РесурсРус, 'у.е.' Валюта UNION
SELECT Ресурсы.КодРесурса КодВалюты, Ресурсы.РесурсРус, ЕдиницыИзмерения.ЕдиницаРус Валюта
FROM Ресурсы
	INNER JOIN Валюты ON Ресурсы.КодРесурса=Валюты.КодВалюты
	INNER JOIN ЕдиницыИзмерения ON Ресурсы.КодЕдиницыИзмерения=ЕдиницыИзмерения.КодЕдиницыИзмерения
";

        #endregion


        #region SQL_ПолучениеИнформацииПоSIM

        public const string SQL_ПолучениеИнформацииПоSIM =
            @"
/*DECLARE @StartDate datetime = '20151129', @EndDate datetime = '20161130', 
		@КодСотрудника int = 50, @НомерТелефона varchar(50) = '9647274479'
*/		
IF @CurrentMonth = 1 
BEGIN	
	SET @StartDate = FLOOR(CONVERT(float,GETDATE()))
	SET @EndDate = @StartDate
END
SELECT	Сотрудники.ФИО Сотрудник, 
		ОборудованиеСотрудников.Примечания,
		ОборудованиеСотрудников.От,
		ОборудованиеСотрудников.До
FROM	vwSIMКарты INNER JOIN
                      ОборудованиеСотрудников ON vwSIMКарты.КодОборудования = ОборудованиеСотрудников.КодОборудования INNER JOIN
                      Сотрудники ON ОборудованиеСотрудников.КодСотрудника = Сотрудники.КодСотрудника
WHERE ОборудованиеСотрудников.От < @EndDate AND ISNULL(ОборудованиеСотрудников.До, CONVERT(datetime,'20500101')) > @StartDate
    AND	(LEN(@НомерТелефона) = 10 AND LEN(vwSIMКарты.НомерТелефона) = 11 AND (vwSIMКарты.НомерТелефона LIKE ('_'+@НомерТелефона)) 
		    OR НомерТелефона = @НомерТелефона)
    AND (ОборудованиеСотрудников.КодСотрудника = @КодСотрудника AND @КодСотрудника > 0 OR @КодСотрудника<=0)
ORDER BY ОборудованиеСотрудников.От
";

        #endregion
    }
}