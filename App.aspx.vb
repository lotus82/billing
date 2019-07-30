Imports System.Data.SqlClient

Public Class App1

    Inherits System.Web.UI.Page
    Public Shared LS_LS As Int64
    Public Shared LS As Int64
    Public Shared OTV As String = ""
    Public Shared ADDR As String = ""
    Public Shared index_LS As Integer = -1
    Public Shared GUID_user As Guid
    Public Shared index_unreg As Integer
    Public Shared index_del_doc As Integer
    Public Shared index_del_counter As Integer
    Public Shared index_del_calc As Integer
    Public Shared index_del_calc_history As Integer
    Public Shared COUNTER_ID As Integer
    Public Shared DropDownList_User_Reg_Edit As DropDownList

    '***********************************************************************************************************************************************************************************************
    '|||||||||||||||||||||||| НАЧАЛО |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    '***********************************************************************************************************************************************************************************************
    '                                                                                                                                                                                              |
    '-------------------------Загрузка страницы-----------------------------------------------------------------------------------------------------------------------------------------------------
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then
            Exit Sub
        End If
        Address(1)
        Dim con1 As String
        con1 = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
        TabContainer1.ActiveTabIndex = 0
        TabContainer_Detail.Visible = False
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Выбор вкладки второго табконтейнера-----------------------------------------------------------------------------------------------------------------------------------
    Protected Sub TabContainer_Detail_ActiveTabChanged(sender As Object, e As EventArgs) Handles TabContainer_Detail.ActiveTabChanged
        '-------------------------Выбор вкладки "Л/C"-----------------------------------------------
        If (TabContainer_Detail.ActiveTabIndex = 0) Then
            OCC_FAMILIA_SEARCH_EDIT(LS)
            UpdatePanel_Table_LS.Update()
            UpdatePanel_Detail.Update()
        End If
        '-------------------------Выбор вкладки "Люди"----------------------------------------------
        If (TabContainer_Detail.ActiveTabIndex = 1) Then
            OCC_FAMILIA_SEARCH_EDIT_USER(LS)
            UpdatePanel_User_info.Update()
            UpdatePanel_Detail.Update()
        End If
        '-------------------------Выбор вкладки "Приборы учета"-------------------------------------
        If (TabContainer_Detail.ActiveTabIndex = 2) Then
            COUNTERS(LS)
            UpdatePanel_Counters.Update()
            UpdatePanel_Detail.Update()
        End If
        '-------------------------Выбор вкладки "Начисления"----------------------------------------
        If (TabContainer_Detail.ActiveTabIndex = 3) Then
            CALC(LS)
            UpdatePanel_Calc.Update()
            UpdatePanel_Detail.Update()
        End If
        '-------------------------Выбор вкладки "История начислений"--------------------------------
        If (TabContainer_Detail.ActiveTabIndex = 4) Then
            CALC_HISTORY(LS)
            UpdatePanel_Calc_History.Update()
            UpdatePanel_Detail.Update()
        End If
        '-------------------------Выбор вкладки "Новая вкладка"----------------------------------------
        If (TabContainer_Detail.ActiveTabIndex = 5) Then
            NEW_PROCEDURE(LS)
            UpdatePanel_New.Update()
            UpdatePanel_Detail.Update()
        End If
    End Sub
    '                                                                                                                                                                                              |
    '***********************************************************************************************************************************************************************************************



    '***********************************************************************************************************************************************************************************************
    '|||||||||||||||||||||||| ВКЛАДКА "ПОИСК ПО АДРЕСУ" ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    '***********************************************************************************************************************************************************************************************
    '                                                                                                                                                                                              |
    '-------------------------Выбор улицы из выпадающего списка-------------------------------------------------------------------------------------------------------------------------------------
    Protected Sub DropDownList_UL_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList_UL.SelectedIndexChanged
        DropDownList_UL.Items.Remove("-Выбор улицы-")
        DropDownList_Bldn.DataSource = ""
        DropDownList_Bldn.DataBind()
        DropDownList_FLT.DataSource = ""
        DropDownList_FLT.DataBind()
        Address(2)
        DropDownList_Bldn.Focus()
        If DropDownList_Bldn.Items.Count > 0 Then
            DropDownList_Bldn.Items.Item(0).Selected = True
            Address(3)
        End If
        If DropDownList_FLT.Items.Count > 0 Then
            ImageButtonAddOCC_ID.Visible = True
            OCC_ADDRESS(DropDownList_FLT.SelectedValue)
        Else
            ImageButtonAddOCC_ID.Visible = False
            OCC_ADDRESS(Convert.ToInt64(0))
            UpdatePanel_LS.Update()
            TabContainer_Detail.Visible = False
            UpdatePanel_Detail.Update()
        End If
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Выбор дома из выпадающего списка--------------------------------------------------------------------------------------------------------------------------------------
    Protected Sub DropDownList_Bldn_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList_Bldn.SelectedIndexChanged
        Address(3)
        If DropDownList_FLT.Items.Count > 0 Then
            ImageButtonAddOCC_ID.Visible = True
            OCC_ADDRESS(DropDownList_FLT.SelectedValue)
        Else
            ImageButtonAddOCC_ID.Visible = False
            OCC_ADDRESS(Convert.ToInt64(0))
            UpdatePanel_LS.Update()
            TabContainer_Detail.Visible = False
            UpdatePanel_Detail.Update()
        End If
        DropDownList_FLT.Focus()
        If (DropDownList_FLT.SelectedIndex > -1) Then
            OCC_ADDRESS(DropDownList_FLT.SelectedValue)
        End If

    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Выбор квартиры из выпадающего списка----------------------------------------------------------------------------------------------------------------------------------
    Protected Sub DropDownList_FLT_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList_FLT.SelectedIndexChanged
        If (DropDownList_FLT.SelectedIndex > -1) Then
            OCC_ADDRESS(DropDownList_FLT.SelectedValue)
        End If
        UpdatePanel_LS.Update()
        TabContainer_Detail.Visible = False
        UpdatePanel_Detail.Update()
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Биндинг квартиры------------------------------------------------------------------------------------------------------------------------------------------------------
    Protected Sub DropDownList_FLT_DataBinding(sender As Object, e As EventArgs) Handles DropDownList_FLT.DataBinding
        If (DropDownList_FLT.SelectedIndex > -1) Then
            OCC_ADDRESS(DropDownList_FLT.SelectedValue)
        End If
        UpdatePanel_LS.Update()
        TabContainer_Detail.Visible = False
        UpdatePanel_Detail.Update()
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Функция выбора улиц, домов и квартир----------------------------------------------------------------------------------------------------------------------------------
    Function Address(type As Integer)
        On Error GoTo err
        Label_danger.Text = ""
        Dim DS As DataSet
        Dim MyConnection As SqlConnection
        Dim MyDataAdapter As SqlDataAdapter
        Dim con As String
        Dim s As String = FormsAuthentication.Decrypt(Request.Cookies("AppBillingCookie").Value).UserData
        con = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
        '********************************************************Улицы
        If type = 1 Then
            MyConnection = New SqlConnection(con)
            MyDataAdapter = New SqlDataAdapter("GET_ADDRESS", MyConnection)
            MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            '1
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@TYPE", SqlDbType.Int))
            MyDataAdapter.SelectCommand.Parameters("@TYPE").Value = type
            '2
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@STREET_ID", SqlDbType.Int))
            MyDataAdapter.SelectCommand.Parameters("@STREET_ID").Value = type
            '3
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@BLDN_ID", SqlDbType.Int))
            MyDataAdapter.SelectCommand.Parameters("@BLDN_ID").Value = type

            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@OPER_GUID", SqlDbType.VarChar, 50))
            MyDataAdapter.SelectCommand.Parameters("@OPER_GUID").Value = s
            '4
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@APP_GUID", SqlDbType.VarChar, 50))
            MyDataAdapter.SelectCommand.Parameters("@APP_GUID").Value = ConfigurationManager.AppSettings.Item("app_guid").ToString
            MyDataAdapter.SelectCommand.CommandTimeout = 600
            DS = New DataSet()
            MyDataAdapter.Fill(DS)
            DropDownList_UL.DataSource = DS.Tables(0)
            DropDownList_UL.DataTextField = "Street"
            DropDownList_UL.DataValueField = "ID_streets"
            DropDownList_UL.ToolTip = "Улицы"
            DropDownList_UL.DataBind()
        End If
        '********************************************************Дома
        If type = 2 Then
            MyConnection = New SqlConnection(con)
            MyDataAdapter = New SqlDataAdapter("GET_ADDRESS", MyConnection)
            MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            '1
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@TYPE", SqlDbType.Int))
            MyDataAdapter.SelectCommand.Parameters("@TYPE").Value = type
            '2
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@STREET_ID", SqlDbType.Int))
            MyDataAdapter.SelectCommand.Parameters("@STREET_ID").Value = DropDownList_UL.SelectedValue
            '3
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@BLDN_ID", SqlDbType.Int))
            MyDataAdapter.SelectCommand.Parameters("@BLDN_ID").Value = type

            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@OPER_GUID", SqlDbType.VarChar, 50))
            MyDataAdapter.SelectCommand.Parameters("@OPER_GUID").Value = s
            '4
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@APP_GUID", SqlDbType.VarChar, 50))
            MyDataAdapter.SelectCommand.Parameters("@APP_GUID").Value = ConfigurationManager.AppSettings.Item("app_guid").ToString
            MyDataAdapter.SelectCommand.CommandTimeout = 600
            DS = New DataSet()
            MyDataAdapter.Fill(DS)
            DropDownList_Bldn.DataSource = DS.Tables(0)
            DropDownList_Bldn.DataTextField = "Build"
            DropDownList_Bldn.DataValueField = "ID_build"
            DropDownList_Bldn.ToolTip = "Дома"
            DropDownList_Bldn.DataBind()
        End If
        '********************************************************Квартиры
        If type = 3 Then
            MyConnection = New SqlConnection(con)
            MyDataAdapter = New SqlDataAdapter("GET_ADDRESS", MyConnection)
            MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            '1
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@TYPE", SqlDbType.Int))
            MyDataAdapter.SelectCommand.Parameters("@TYPE").Value = type
            '2
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@STREET_ID", SqlDbType.Int))
            MyDataAdapter.SelectCommand.Parameters("@STREET_ID").Value = DropDownList_UL.SelectedValue
            '3
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@BLDN_ID", SqlDbType.Int))
            MyDataAdapter.SelectCommand.Parameters("@BLDN_ID").Value = DropDownList_Bldn.SelectedValue

            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@OPER_GUID", SqlDbType.VarChar, 50))
            MyDataAdapter.SelectCommand.Parameters("@OPER_GUID").Value = s
            '4
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@APP_GUID", SqlDbType.VarChar, 50))
            MyDataAdapter.SelectCommand.Parameters("@APP_GUID").Value = ConfigurationManager.AppSettings.Item("app_guid").ToString
            MyDataAdapter.SelectCommand.CommandTimeout = 600
            DS = New DataSet()
            MyDataAdapter.Fill(DS)
            DropDownList_FLT.DataSource = DS.Tables(0)
            DropDownList_FLT.DataTextField = "Flat"
            DropDownList_FLT.DataValueField = "ID_flats"
            DropDownList_FLT.ToolTip = "Квартиры"
            DropDownList_FLT.DataBind()
        End If

        Return Nothing
err:
        Label_danger.Text = Err.Description
    End Function
    '                                                                                                                                                                                              |
    '-------------------------Функция "Поиск по адресу"---------------------------------------------------------------------------------------------------------------------------------------------
    Protected Sub OCC_ADDRESS(Flat_LS As Integer)
        Dim MyConnection As SqlConnection
        Dim MyDataAdapter As SqlDataAdapter
        Dim con1 As String
        Dim s1 As String = FormsAuthentication.Decrypt(Request.Cookies("AppBillingCookie").Value).UserData
        con1 = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
        MyConnection = New SqlConnection(con1)
        MyDataAdapter = New SqlDataAdapter("APP_OCC_ADDRESS", MyConnection)
        MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Flat_LS", SqlDbType.VarChar, 50))
        If DropDownList_FLT.SelectedIndex > -1 Then
            MyDataAdapter.SelectCommand.Parameters("@Flat_LS").Value = Flat_LS
        Else
            MyDataAdapter.SelectCommand.Parameters("@Flat_LS").Value = 0
        End If
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@OPER_GUID", SqlDbType.VarChar, 50))
        MyDataAdapter.SelectCommand.Parameters("@OPER_GUID").Value = s1
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@APP_GUID", SqlDbType.VarChar, 50))
        MyDataAdapter.SelectCommand.Parameters("@APP_GUID").Value = ConfigurationManager.AppSettings.Item("app_guid").ToString
        '---------------------------------------------------------------------------------------
        Dim DS As DataSet
        DS = New DataSet()
        MyDataAdapter.Fill(DS)
        GridView_LS.DataSource = DS.Tables(0).DefaultView
        GridView_LS.AutoGenerateColumns = False
        GridView_LS.Visible = True
        GridView_LS.DataBind()
        For i As Integer = 0 To DS.Tables(0).Rows.Count - 1
            If DS.Tables(0).Rows.Item(i).Item(3) = 1 Then
                GridView_LS.Rows.Item(i).BackColor = Drawing.Color.LightGray
            Else : GridView_LS.Rows.Item(i).BackColor = Drawing.Color.White
            End If
        Next
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Нажатие на картинку "Добавить Л/С" (вызов модального окна "Добавление нового Л/С")------------------------------------------------------------------------------------
    Protected Sub ImageButtonAddOCC_ID_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButtonAddOCC_ID.Click
        If IsNumeric(DropDownList_FLT.SelectedValue) = True Then
            TextBoxSQALL.Text = ""
            TextBoxSQLIving.Text = ""
            TextBoxSQMOP.Text = ""
            TextBoxLName.Text = ""
            TextBoxFName.Text = ""
            TextBoxSNmame.Text = ""
            Label_Add_OCC.Text = ""
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalAddOCC_ID", "$('#myModalAddOCC_ID').modal({backdrop: 'static',keyboard: false });", True)
            lblModalOCC_ID.Text = "Новый Л/С по адресу: " & DropDownList_UL.SelectedItem.Text & " д. " & DropDownList_Bldn.SelectedItem.Text & " кв. " & DropDownList_FLT.SelectedItem.Text
            UpdatePanel_AddOCC_ID.Update()
        End If
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Нажатие на кнопку "Добавить" в модальном окне "Добавление нового Л/С"-------------------------------------------------------------------------------------------------
    Protected Sub ButtonAddOCC_ID_Click(sender As Object, e As EventArgs)
        If Convert.ToDecimal(TextBoxSQLIving.Text) > Convert.ToDecimal(TextBoxSQALL.Text) Then
            Label_Add_OCC.Text = "Жилая площадь не может быть больше общей."
            UpdatePanel_AddOCC_ID.Update()
            Exit Sub
        End If
        Dim user_new_guid As Guid = Guid.NewGuid()
        Dim MyConnection As SqlConnection
        Dim MyDataAdapter As SqlDataAdapter
        Dim con As String
        Dim s As String = FormsAuthentication.Decrypt(Request.Cookies("AppBillingCookie").Value).UserData
        con = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
        MyConnection = New SqlConnection(con)
        MyDataAdapter = New SqlDataAdapter("APP_OCC_ADD", MyConnection)
        MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
        Dim R As New Random
        'MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ID", SqlDbType.BigInt))
        'MyDataAdapter.SelectCommand.Parameters("@ID").Value = R.Next(7000000, 7999999)
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@FLAT_ID", SqlDbType.Int))
        MyDataAdapter.SelectCommand.Parameters("@FLAT_ID").Value = DropDownList_FLT.SelectedValue
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@OCC_TYPE_ID", SqlDbType.Int))
        MyDataAdapter.SelectCommand.Parameters("@OCC_TYPE_ID").Value = DropDownListOCC_TYPE.SelectedValue
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@OCC_STATUS_ID", SqlDbType.Int))
        MyDataAdapter.SelectCommand.Parameters("@OCC_STATUS_ID").Value = DropDownListOCC_STATUS.SelectedValue
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@OCC_PROPERTY_ID", SqlDbType.Char, 4))
        MyDataAdapter.SelectCommand.Parameters("@OCC_PROPERTY_ID").Value = DropDownListPRIV.SelectedValue
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@SQ", SqlDbType.Decimal, 18, 4))
        MyDataAdapter.SelectCommand.Parameters("@SQ").Value = Convert.ToDecimal(TextBoxSQALL.Text)
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@LIVING_SQ", SqlDbType.Decimal, 18, 4))
        MyDataAdapter.SelectCommand.Parameters("@LIVING_SQ").Value = Convert.ToDecimal(TextBoxSQLIving.Text)
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@MOP_SQ", SqlDbType.Decimal, 18, 4))
        MyDataAdapter.SelectCommand.Parameters("@MOP_SQ").Value = Convert.ToDecimal(TextBoxSQMOP.Text)
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@START_DATE", SqlDbType.Date))
        MyDataAdapter.SelectCommand.Parameters("@START_DATE").Value = TextBoxDateReg.Text
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@IS_CLOSED", SqlDbType.Int))
        MyDataAdapter.SelectCommand.Parameters("@IS_CLOSED").Value = 0
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@GUID_user", SqlDbType.UniqueIdentifier))
        MyDataAdapter.SelectCommand.Parameters("@GUID_user").Value = user_new_guid
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Surname", SqlDbType.VarChar, 50))
        MyDataAdapter.SelectCommand.Parameters("@Surname").Value = TextBoxLName.Text
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Name", SqlDbType.VarChar, 50))
        MyDataAdapter.SelectCommand.Parameters("@Name").Value = TextBoxFName.Text
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Patronymic", SqlDbType.VarChar, 50))
        MyDataAdapter.SelectCommand.Parameters("@Patronymic").Value = TextBoxSNmame.Text
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Email", SqlDbType.VarChar, 50))
        MyDataAdapter.SelectCommand.Parameters("@Email").Value = vbNull
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Date_birth", SqlDbType.Date))
        MyDataAdapter.SelectCommand.Parameters("@Date_birth").Value = TextBoxB_Date.Text
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Sex", SqlDbType.Int))
        MyDataAdapter.SelectCommand.Parameters("@Sex").Value = 0
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Phone", SqlDbType.VarChar, 12))
        MyDataAdapter.SelectCommand.Parameters("@Phone").Value = vbNull
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ID_doc", SqlDbType.Int))
        MyDataAdapter.SelectCommand.Parameters("@ID_doc").Value = 0
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@ID_registration_type", SqlDbType.Int))
        MyDataAdapter.SelectCommand.Parameters("@ID_registration_type").Value = DropDownListP_Status.SelectedValue
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@Date_registration", SqlDbType.Date))
        MyDataAdapter.SelectCommand.Parameters("@Date_registration").Value = TextBoxDateReg.Text
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@OPER_GUID", SqlDbType.VarChar, 50))
        MyDataAdapter.SelectCommand.Parameters("@OPER_GUID").Value = s
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@APP_GUID", SqlDbType.VarChar, 50))
        MyDataAdapter.SelectCommand.Parameters("@APP_GUID").Value = ConfigurationManager.AppSettings.Item("app_guid").ToString
        MyConnection.Open()
        MyDataAdapter.SelectCommand.ExecuteNonQuery()
        MyConnection.Close()

        Label_Add_OCC.Text = ""
        If TextBoxSQALL.Text.Length = 0 Then
            TextBoxSQALL.Text = 0
        End If
        If TextBoxSQLIving.Text.Length = 0 Then
            TextBoxSQLIving.Text = 0
        End If
        If TextBoxSQMOP.Text.Length = 0 Then
            TextBoxSQMOP.Text = 0
        End If
        UpdatePanel_AddOCC_ID.Update()
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalAddOCC_ID", "$('#myModalAddOCC_ID').modal('hide');", True)

        OCC_ADDRESS(DropDownList_FLT.SelectedValue)
        UpdatePanel1.Update()
        UpdatePanel_LS.Update()
        TabContainer_Detail.Visible = False
        UpdatePanel_Detail.Update()

    End Sub
    '                                                                                                                                                                                              |
    '***********************************************************************************************************************************************************************************************



    '***********************************************************************************************************************************************************************************************
    '|||||||||||||||||||||||| ВКЛАДКА "ПОИСК ПО ЛИЦЕВОМУ СЧЕТУ" ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    '***********************************************************************************************************************************************************************************************
    '                                                                                                                                                                                              |
    '-------------------------Функция "Поиск по Л/С"------------------------------------------------------------------------------------------------------------------------------------------------
    Protected Sub OCC_LS(LS As Integer, LS_MAX As Integer)
        Dim MyConnection As SqlConnection
        Dim MyDataAdapter As SqlDataAdapter
        Dim con1 As String
        Dim s1 As String = FormsAuthentication.Decrypt(Request.Cookies("AppBillingCookie").Value).UserData
        con1 = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
        MyConnection = New SqlConnection(con1)
        MyDataAdapter = New SqlDataAdapter("APP_OCC_LS", MyConnection)
        MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@LS", SqlDbType.Int))
        If TextBox1.Text.Length > 0 Then
            MyDataAdapter.SelectCommand.Parameters("@LS").Value = Convert.ToInt32(TextBox1.Text)
        Else
            MyDataAdapter.SelectCommand.Parameters("@LS").Value = vbNull
        End If
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@OPER_GUID", SqlDbType.VarChar, 50))
        MyDataAdapter.SelectCommand.Parameters("@OPER_GUID").Value = s1
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@APP_GUID", SqlDbType.VarChar, 50))
        MyDataAdapter.SelectCommand.Parameters("@APP_GUID").Value = ConfigurationManager.AppSettings.Item("app_guid").ToString
        '---------------------------------------------------------------------------------------
        Dim DS As DataSet
        DS = New DataSet()
        MyDataAdapter.Fill(DS)
        GridView_LS.DataSource = DS.Tables(0).DefaultView
        GridView_LS.AutoGenerateColumns = False
        GridView_LS.Visible = True
        GridView_LS.DataBind()
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Нажатие на кнопку "Найти" (Поиск по Л/С)------------------------------------------------------------------------------------------------------------------------------
    Protected Sub Button_LS_Click(sender As Object, e As EventArgs) Handles Button_LS.Click
        Dim LS_MAX As Integer
        If TextBox1.Text.Length > 0 Then
            LS_LS = Convert.ToInt32(TextBox1.Text.PadRight(7).Replace(" ", "0"))
        Else
            LS_LS = vbNull
        End If
        If TextBox1.Text.Length > 0 Then
            LS_MAX = Convert.ToInt32(TextBox1.Text.PadRight(7).Replace(" ", "9"))
        Else
            LS_MAX = vbNull
        End If
        OCC_LS(LS_LS, LS_MAX)
        UpdatePanel_Detail.Update()
        UpdatePanel_LS.Update()
        TabContainer_Detail.Visible = False
    End Sub
    '                                                                                                                                                                                              |
    '***********************************************************************************************************************************************************************************************



    '***********************************************************************************************************************************************************************************************
    '|||||||||||||||||||||||| ВКЛАДКА "ПОИСК ПО ФАМИЛИИ" |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    '***********************************************************************************************************************************************************************************************
    '                                                                                                                                                                                              |
    '-------------------------Функция "Поиск по фамилии"--------------------------------------------------------------------------------------------------------------------------------------------
    Protected Sub OCC_FAMILIA()
        Dim MyConnection As SqlConnection
        Dim MyDataAdapter As SqlDataAdapter
        Dim con1 As String
        Dim s1 As String = FormsAuthentication.Decrypt(Request.Cookies("AppBillingCookie").Value).UserData
        con1 = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
        MyConnection = New SqlConnection(con1)
        MyDataAdapter = New SqlDataAdapter("APP_OCC_FAMILIA", MyConnection)
        MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@FAMILIA", SqlDbType.VarChar, 50))
        If TextBox5.Text.Length > 0 Then
            MyDataAdapter.SelectCommand.Parameters("@FAMILIA").Value = TextBox5.Text
        Else
            MyDataAdapter.SelectCommand.Parameters("@FAMILIA").Value = ""
        End If
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@N", SqlDbType.VarChar, 50))
        If TextBox6.Text.Length > 0 Then
            MyDataAdapter.SelectCommand.Parameters("@N").Value = TextBox6.Text
        Else
            MyDataAdapter.SelectCommand.Parameters("@N").Value = ""
        End If
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@P", SqlDbType.VarChar, 50))
        If TextBox7.Text.Length > 0 Then
            MyDataAdapter.SelectCommand.Parameters("@P").Value = TextBox7.Text
        Else
            MyDataAdapter.SelectCommand.Parameters("@P").Value = ""
        End If
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@OPER_GUID", SqlDbType.VarChar, 50))
        MyDataAdapter.SelectCommand.Parameters("@OPER_GUID").Value = s1
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@APP_GUID", SqlDbType.VarChar, 50))
        MyDataAdapter.SelectCommand.Parameters("@APP_GUID").Value = ConfigurationManager.AppSettings.Item("app_guid").ToString
        '---------------------------------------------------------------------------------------
        Dim DS As DataSet
        DS = New DataSet()
        MyDataAdapter.Fill(DS)
        GridView3.DataSource = DS.Tables(0).DefaultView
        GridView3.AutoGenerateColumns = False
        GridView3.Visible = True
        GridView3.DataBind()
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Функция "Выбор Л/С по фамилии"----------------------------------------------------------------------------------------------------------------------------------------
    Protected Sub OCC_FAMILIA_EDIT(LS As Int64)
        Dim MyConnection As SqlConnection
        Dim MyDataAdapter As SqlDataAdapter
        Dim con1 As String
        Dim s1 As String = FormsAuthentication.Decrypt(Request.Cookies("AppBillingCookie").Value).UserData
        con1 = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
        MyConnection = New SqlConnection(con1)
        MyDataAdapter = New SqlDataAdapter("APP_OCC_LS", MyConnection)
        MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@LS", SqlDbType.BigInt))
        MyDataAdapter.SelectCommand.Parameters("@LS").Value = LS
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@OPER_GUID", SqlDbType.VarChar, 50))
        MyDataAdapter.SelectCommand.Parameters("@OPER_GUID").Value = s1
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@APP_GUID", SqlDbType.VarChar, 50))
        MyDataAdapter.SelectCommand.Parameters("@APP_GUID").Value = ConfigurationManager.AppSettings.Item("app_guid").ToString
        '---------------------------------------------------------------------------------------
        Dim DS As DataSet
        DS = New DataSet()
        MyDataAdapter.Fill(DS)
        GridView_LS.DataSource = DS.Tables(0).DefaultView
        GridView_LS.AutoGenerateColumns = False
        GridView_LS.Visible = True
        GridView_LS.DataBind()
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Нажатие на кнопку "Найти" (вызов модального окна "Поиск по фамилии")--------------------------------------------------------------------------------------------------
    Protected Sub Button_Familia_Click(sender As Object, e As EventArgs) Handles Button_Familia.Click
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalSEARCH_OCC_FAMILIA", "$('#myModalSEARCH_OCC_FAMILIA').modal({backdrop: 'static',keyboard: false });", True)
        Label3.Text = "Поиск Л/С по ФИО: "
        TextBox5.Text = ""
        TextBox6.Text = ""
        TextBox7.Text = ""
        UpdatePanel4.Update()
        UpdatePanel3.Update()
        GridView3.DataBind()
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Нажатие на кнопку "Найти" в модальном окне "Поиск по фамилии"---------------------------------------------------------------------------------------------------------
    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        OCC_FAMILIA()
        UpdatePanel3.Update()
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Клик по картинке "Выбрать" в гриде модального окна "Поиск по фамилии"-------------------------------------------------------------------------------------------------
    Protected Sub GridView3_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView3.RowCommand
        If (e.CommandName = "edit_click") Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim LS As Int64 = Convert.ToInt64(GridView3.DataKeys(index).Value)
            Dim row As GridViewRow = GridView3.Rows(index)
            Label_search_familia.Text = "Информация по лицевому счету № " + """" + LS.ToString() + """"
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalSEARCH_OCC_FAMILIA", "$('#myModalSEARCH_OCC_FAMILIA').modal('hide');", True)
            OCC_FAMILIA_EDIT(LS)
            'LS_FAMILIA = LS
            TabContainer_Detail.Visible = False
            UpdatePanel_Detail.Update()
            UpdatePanel_LS.Update()
        End If
    End Sub
    '                                                                                                                                                                                              |
    '***********************************************************************************************************************************************************************************************


    
    '***********************************************************************************************************************************************************************************************
    '|||||||||||||||||||||||| ГРИД "ЛИЦЕВОЙ СЧЕТ" ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    '***********************************************************************************************************************************************************************************************
    '                                                                                                                                                                                              |
    '-------------------------Клик по картинке "Выбрать" в гриде "Л/С"------------------------------------------------------------------------------------------------------------------------------
    Protected Sub GridView_LS_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView_LS.RowCommand
        If (e.CommandName = "edit_click") Then
            index_LS = Convert.ToInt32(e.CommandArgument)
            For i As Integer = 0 To GridView_LS.Rows.Count - 1
                If i = index_LS Then
                    If GridView_LS.Rows.Item(i).BackColor = Drawing.Color.White Then
                        GridView_LS.Rows.Item(i).BackColor = Drawing.Color.Yellow
                    End If
                    If GridView_LS.Rows.Item(i).BackColor = Drawing.Color.LightGray Then
                        GridView_LS.Rows.Item(i).BackColor = Drawing.Color.YellowGreen
                    End If
                Else
                    If GridView_LS.Rows.Item(i).BackColor = Drawing.Color.Yellow Then
                        GridView_LS.Rows.Item(i).BackColor = Drawing.Color.White
                    End If
                    If GridView_LS.Rows.Item(i).BackColor = Drawing.Color.YellowGreen Then
                        GridView_LS.Rows.Item(i).BackColor = Drawing.Color.LightGray
                    End If
                End If
            Next
            Dim row As GridViewRow = GridView_LS.Rows(index_LS)
            LS = Convert.ToInt64(GridView_LS.DataKeys(index_LS).Value)
            OTV = row.Cells.Item(2).Text
            ADDR = row.Cells.Item(3).Text
            If (TabContainer_Detail.ActiveTabIndex = 0) Then
                OCC_FAMILIA_SEARCH_EDIT(LS)
            End If
            If (TabContainer_Detail.ActiveTabIndex = 1) Then
                OCC_FAMILIA_SEARCH_EDIT_USER(LS)
            End If
            If (TabContainer_Detail.ActiveTabIndex = 2) Then
                COUNTERS(LS)
            End If
            If (TabContainer_Detail.ActiveTabIndex = 3) Then
                CALC(LS)
            End If
            TabContainer_Detail.Visible = True
            GridView_Doc.Visible = False
            Button_Doc_Add.Visible = False
            UpdatePanel_LS.Update()
            UpdatePanel_User_info.Update()
            UpdatePanel_Detail.Update()
        End If
    End Sub
    '                                                                                                                                                                                              |
    '***********************************************************************************************************************************************************************************************



    '***********************************************************************************************************************************************************************************************
    '|||||||||||||||||||||||| ВКЛАДКА "ЛИЦЕВОЙ СЧЕТ" |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    '***********************************************************************************************************************************************************************************************
    '                                                                                                                                                                                              |
    '-------------------------Функция (Заполнение вкладки "ЛИЦЕВОЙ СЧЕТ")---------------------------------------------------------------------------------------------------------------------------
    Protected Sub OCC_FAMILIA_SEARCH_EDIT(LS As Int64)
        Label_LS_info_header.Text = "Л/С №" & LS.ToString() & " (ответственный " & OTV & ") адрес " & ADDR
        Dim MyConnection As SqlConnection
        Dim MyDataAdapter As SqlDataAdapter
        Dim con1 As String
        Dim s1 As String = FormsAuthentication.Decrypt(Request.Cookies("AppBillingCookie").Value).UserData
        con1 = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
        MyConnection = New SqlConnection(con1)
        MyDataAdapter = New SqlDataAdapter("APP_OCC_LS_FAMILIA", MyConnection)
        MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@LS", SqlDbType.BigInt))
        MyDataAdapter.SelectCommand.Parameters("@LS").Value = LS
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@OPER_GUID", SqlDbType.VarChar, 50))
        MyDataAdapter.SelectCommand.Parameters("@OPER_GUID").Value = s1 & vbCr
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@APP_GUID", SqlDbType.VarChar, 50))
        MyDataAdapter.SelectCommand.Parameters("@APP_GUID").Value = ConfigurationManager.AppSettings.Item("app_guid").ToString
        '---------------------------------------------------------------------------------------
        Dim DS As DataSet
        DS = New DataSet()
        MyDataAdapter.Fill(DS)
        TextBox_Familia_Serch_1_ID.Text = DS.Tables(0).Rows.Item(0).Item(0).ToString()
        TextBox_Dog_num.Text = DS.Tables(0).Rows.Item(0).Item(1).ToString()
        TextBox_SQ.Text = DS.Tables(0).Rows.Item(0).Item(7).ToString()
        TextBox_SQ_live.Text = DS.Tables(0).Rows.Item(0).Item(8).ToString()
        TextBox_SQ_hot.Text = DS.Tables(0).Rows.Item(0).Item(9).ToString()
        TextBox_SQ_MOP.Text = DS.Tables(0).Rows.Item(0).Item(10).ToString()
        TextBox_Real_live.Text = DS.Tables(0).Rows.Item(0).Item(11).ToString()
        TextBox_Email.Text = DS.Tables(0).Rows.Item(0).Item(12).ToString()
        TextBox_Phone.Text = DS.Tables(0).Rows.Item(0).Item(13).ToString()
        TextBox_Phone2.Text = DS.Tables(0).Rows.Item(0).Item(14).ToString()
        TextBox_Kadastr.Text = DS.Tables(0).Rows.Item(0).Item(15).ToString()
        TextBox_GIS.Text = DS.Tables(0).Rows.Item(0).Item(16).ToString()
        TextBox_Code.Text = DS.Tables(0).Rows.Item(0).Item(17).ToString()
        TextBox_Start_date.Text = Format(DS.Tables(0).Rows.Item(0).Item(18).ToString(), "{0:yyyy-MM-dd}")
        TextBox_Start_date.Text = Left(TextBox_Start_date.Text, 10)
        TextBox_End_date.Text = Format(DS.Tables(0).Rows.Item(0).Item(19).ToString(), "{0:yyyy-MM-dd}")
        TextBox_End_date.Text = Left(TextBox_End_date.Text, 10)
        If DS.Tables(0).Rows.Item(0).Item(20).ToString().Length > 20 Then
            Label1_Is_closed_info.Text = Left(DS.Tables(0).Rows.Item(0).Item(21).ToString(), 15) & "..."
        Else
            Label1_Is_closed_info.Text = DS.Tables(0).Rows.Item(0).Item(21).ToString()
        End If
        Dim MyConnection2 As SqlConnection
        Dim MyDataAdapter2 As SqlDataAdapter
        Dim con2 As String
        Dim s2 As String = FormsAuthentication.Decrypt(Request.Cookies("AppBillingCookie").Value).UserData
        con2 = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
        MyConnection2 = New SqlConnection(con2)
        MyDataAdapter2 = New SqlDataAdapter("APP_OCC_type", MyConnection2)
        MyDataAdapter2.SelectCommand.CommandType = CommandType.StoredProcedure
        '---------------------------------------------------------------------------------------
        MyDataAdapter2.SelectCommand.Parameters.Add(New SqlParameter("@OPER_GUID", SqlDbType.VarChar, 50))
        MyDataAdapter2.SelectCommand.Parameters("@OPER_GUID").Value = s2
        '---------------------------------------------------------------------------------------
        MyDataAdapter2.SelectCommand.Parameters.Add(New SqlParameter("@APP_GUID", SqlDbType.VarChar, 50))
        MyDataAdapter2.SelectCommand.Parameters("@APP_GUID").Value = ConfigurationManager.AppSettings.Item("app_guid").ToString
        '---------------------------------------------------------------------------------------
        Dim DS2 As DataSet
        DS2 = New DataSet()
        MyDataAdapter2.Fill(DS2)
        Dim dv1 As DataView = New DataView(DS2.Tables(0))
        DropDownList_OCC_type.DataSource = dv1
        DropDownList_OCC_type.DataTextField = "OCC_TYPE_NAME"
        DropDownList_OCC_type.DataValueField = "ID"
        DropDownList_OCC_type.DataBind()
        DropDownList_OCC_type.SelectedValue = DS.Tables(0).Rows.Item(0).Item(4)
        Dim dv2 As DataView = New DataView(DS2.Tables(1))
        DropDownList_OCC_status.DataSource = dv2
        DropDownList_OCC_status.DataTextField = "OCC_STATUS_NAME"
        DropDownList_OCC_status.DataValueField = "ID"
        DropDownList_OCC_status.DataBind()
        DropDownList_OCC_status.SelectedValue = DS.Tables(0).Rows.Item(0).Item(5)
        Dim dv3 As DataView = New DataView(DS2.Tables(2))
        DropDownList_OCC_property.DataSource = dv3
        DropDownList_OCC_property.DataTextField = "PROPERTY_NAME"
        DropDownList_OCC_property.DataValueField = "ID"
        DropDownList_OCC_property.DataBind()
        DropDownList_OCC_property.SelectedValue = DS.Tables(0).Rows.Item(0).Item(6)
        Dim dv4 As DataView = New DataView(DS2.Tables(3))
        DropDownList_Is_closed.DataSource = dv4
        DropDownList_Is_closed.DataTextField = "Is_closed"
        DropDownList_Is_closed.DataValueField = "ID"
        DropDownList_Is_closed.DataBind()
        DropDownList_Is_closed.SelectedValue = DS.Tables(0).Rows.Item(0).Item(20)
        If (DropDownList_Is_closed.SelectedValue = 1) Then
            TabContainer_Detail.Enabled = False
            Label1_Is_closed_info.Visible = True
            'TextBox_End_date.Visible = True
        ElseIf (DropDownList_Is_closed.SelectedValue = 0) Then
            TabContainer_Detail.Enabled = True
            Label1_Is_closed_info.Visible = False
            'TextBox_End_date.Visible = False
        End If
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Функция "РЕДАКТИРОВАНИЕ ЛИЦЕВОГО СЧЕТА"-------------------------------------------------------------------------------------------------------------------------------
    Protected Sub OCC_UPDATE(LS As Int64)
        'Label_SQ_info.Text = ""
        'Label_SQ_hot_info.Text = ""
        'Label_SQ_live_info.Text = ""
        'Label_SQ_MOP_info.Text = ""
        'Label_Phone_info.Text = ""
        'Label_Phone2_info.Text = ""
        'Label_Email_info.Text = ""
        'Label_SQ_info.ForeColor = Drawing.Color.Black
        'Label_SQ_hot_info.ForeColor = Drawing.Color.Black
        'Label_SQ_live_info.ForeColor = Drawing.Color.Black
        'UpdatePanel_Table_LS.Update()
        Dim MyConnection As SqlConnection
        'Dim MyDataAdapter As SqlDataAdapter
        Dim MySqlComand As SqlCommand
        Dim con1 As String
        Dim s1 As String = FormsAuthentication.Decrypt(Request.Cookies("AppBillingCookie").Value).UserData
        con1 = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
        MyConnection = New SqlConnection(con1)
        MySqlComand = New SqlCommand("APP_OCC_UPDATE", MyConnection)
        'MyDataAdapter = New SqlDataAdapter("APP_OCC_UPDATE", MyConnection)
        'MyDataAdapter.UpdateCommand.CommandType = CommandType.StoredProcedure
        MySqlComand.CommandType = CommandType.StoredProcedure
        '---------------------------------------------------------------------------------------
        MySqlComand.Parameters.Add(New SqlParameter("@LS", SqlDbType.BigInt))
        MySqlComand.Parameters("@LS").Value = LS
        '---------------------------------------------------------------------------------------
        MySqlComand.Parameters.Add(New SqlParameter("@OPER_GUID", SqlDbType.VarChar, 50))
        MySqlComand.Parameters("@OPER_GUID").Value = s1
        '---------------------------------------------------------------------------------------
        MySqlComand.Parameters.Add(New SqlParameter("@APP_GUID", SqlDbType.VarChar, 50))
        MySqlComand.Parameters("@APP_GUID").Value = ConfigurationManager.AppSettings.Item("app_guid").ToString
        '---------------------------------------------------------------------------------------
        MySqlComand.Parameters.Add(New SqlParameter("@HOST", SqlDbType.VarChar, 50))
        MySqlComand.Parameters("@HOST").Value = HttpContext.Current.Request.UserHostAddress
        '---------------------------------------------------------------------------------------
        MySqlComand.Parameters.Add(New SqlParameter("@DOG_NUM", SqlDbType.VarChar, 32))
        MySqlComand.Parameters("@DOG_NUM").Value = TextBox_Dog_num.Text
        '---------------------------------------------------------------------------------------
        MySqlComand.Parameters.Add(New SqlParameter("@OCC_TYPE_ID", SqlDbType.Int))
        MySqlComand.Parameters("@OCC_TYPE_ID").Value = DropDownList_OCC_type.SelectedValue
        '---------------------------------------------------------------------------------------
        MySqlComand.Parameters.Add(New SqlParameter("@OCC_STATUS_ID", SqlDbType.Int))
        MySqlComand.Parameters("@OCC_STATUS_ID").Value = DropDownList_OCC_status.SelectedValue
        '---------------------------------------------------------------------------------------
        MySqlComand.Parameters.Add(New SqlParameter("@OCC_PROPERTY_ID", SqlDbType.Char, 4))
        MySqlComand.Parameters("@OCC_PROPERTY_ID").Value = DropDownList_OCC_property.SelectedValue.ToString()
        '---------------------------------------------------------------------------------------
        MySqlComand.Parameters.Add(New SqlParameter("@SQ", SqlDbType.Decimal, 18, 2))
        If (Convert.ToDecimal(TextBox_SQ.Text) >= Convert.ToDecimal(TextBox_SQ_live.Text)) Then
            MySqlComand.Parameters("@SQ").Value = Convert.ToDecimal(TextBox_SQ.Text)
        Else
            Label_SQ_info.ForeColor = Drawing.Color.Red
            Label_SQ_info.Text = "Общая площадь меньше жилой площади!!!"
            Exit Sub
        End If
        '---------------------------------------------------------------------------------------
        MySqlComand.Parameters.Add(New SqlParameter("@HOT_SQ", SqlDbType.Decimal, 18, 2))
        If (Convert.ToDecimal(TextBox_SQ_hot.Text) >= Convert.ToDecimal(TextBox_SQ.Text)) Then
            MySqlComand.Parameters("@HOT_SQ").Value = Convert.ToDecimal(TextBox_SQ_hot.Text)
        Else
            Label_SQ_hot_info.ForeColor = Drawing.Color.Red
            Label_SQ_hot_info.Text = "Отапливаемая площадь меньше общей!!!"
            Exit Sub
        End If
        MySqlComand.Parameters.Add(New SqlParameter("@LIVING_SQ", SqlDbType.Decimal, 18, 2))
        If (Convert.ToDecimal(TextBox_SQ_live.Text) <= Convert.ToDecimal(TextBox_SQ.Text)) Then
            MySqlComand.Parameters("@LIVING_SQ").Value = Convert.ToDecimal(TextBox_SQ_live.Text)
        Else
            Label_SQ_live_info.ForeColor = Drawing.Color.Red
            Label_SQ_live_info.Text = "Жилая площадь превышает общую!!!"
            Exit Sub
        End If
        '---------------------------------------------------------------------------------------
        MySqlComand.Parameters.Add(New SqlParameter("@MOP_SQ", SqlDbType.Decimal, 18, 2))
        MySqlComand.Parameters("@MOP_SQ").Value = Convert.ToDecimal(TextBox_SQ_MOP.Text)
        '---------------------------------------------------------------------------------------
        MySqlComand.Parameters.Add(New SqlParameter("@MAIL", SqlDbType.VarChar, 50))
        MySqlComand.Parameters("@MAIL").Value = TextBox_Email.Text
        '---------------------------------------------------------------------------------------
        MySqlComand.Parameters.Add(New SqlParameter("@PHONE", SqlDbType.VarChar, 50))
        MySqlComand.Parameters("@PHONE").Value = TextBox_Phone.Text
        '---------------------------------------------------------------------------------------
        MySqlComand.Parameters.Add(New SqlParameter("@PHONE2", SqlDbType.VarChar, 50))
        MySqlComand.Parameters("@PHONE2").Value = TextBox_Phone2.Text
        '---------------------------------------------------------------------------------------
        MySqlComand.Parameters.Add(New SqlParameter("@CADASTRAL_CODE", SqlDbType.VarChar, 50))
        MySqlComand.Parameters("@CADASTRAL_CODE").Value = TextBox_Kadastr.Text
        '---------------------------------------------------------------------------------------
        MySqlComand.Parameters.Add(New SqlParameter("@GIS_ELS", SqlDbType.VarChar, 64))
        MySqlComand.Parameters("@GIS_ELS").Value = TextBox_GIS.Text
        '---------------------------------------------------------------------------------------
        MySqlComand.Parameters.Add(New SqlParameter("@EXTERNAL_CODE", SqlDbType.VarChar, 32))
        MySqlComand.Parameters("@EXTERNAL_CODE").Value = TextBox_Code.Text
        '---------------------------------------------------------------------------------------
        MySqlComand.Parameters.Add(New SqlParameter("@IS_CLOSED", SqlDbType.Int))
        MySqlComand.Parameters("@IS_CLOSED").Value = DropDownList_Is_closed.SelectedValue
        '---------------------------------------------------------------------------------------
        MySqlComand.Parameters.Add(New SqlParameter("@REASON_ARHIV", SqlDbType.VarChar, 100))
        MySqlComand.Parameters("@REASON_ARHIV").Value = TextBox_Reason_arhiv.Text
        '---------------------------------------------------------------------------------------
        Label_SQ_info.Text = ""
        Label_SQ_hot_info.Text = ""
        Label_SQ_live_info.Text = ""
        Label_SQ_MOP_info.Text = ""
        Label_Phone_info.Text = ""
        Label_Phone2_info.Text = ""
        Label_Email_info.Text = ""
        MyConnection.Open()
        MySqlComand.ExecuteNonQuery()
        MyConnection.Close()
        'UpdatePanel_Table_LS.Update()
        'UpdatePanel_Detail.Update()
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Нажатие на кнопку "В архив" модального окна "АРХИВАЦИЯ ЛИЦЕВОГО СЧЕТА"------------------------------------------------------------------------------------------------
    Protected Sub Button_myModal_Arhiv_LS_Click(sender As Object, e As EventArgs) ' Handles Button_myModal_Arhiv_LS.Click
        Label_Reason_Arhiv.ForeColor = Drawing.Color.Black
        Label_Reason_Arhiv.Text = "Укажите причину"
        If TextBox_Reason_arhiv.Text.Length = 0 Then
            Label_Reason_Arhiv.ForeColor = Drawing.Color.Red
            Label_Reason_Arhiv.Text = "Вы не указали причину!!!"
            UpdatePanel_myModal_Arhiv_LS.Update()
            Exit Sub
        End If
        OCC_UPDATE(LS)
        OCC_FAMILIA_SEARCH_EDIT(LS)
        UpdatePanel_Table_LS.Update()
        GridView_LS.Rows.Item(index_LS).BackColor = Drawing.Color.YellowGreen
        UpdatePanel_LS.Update()
        'UpdatePanel_Detail.Update()
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal_Arhiv_LS", "$('#myModal_Arhiv_LS').modal('hide');", True)
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Валидация поля "Номер договора"---------------------------------------------------------------------------------------------------------------------------------------
    Protected Sub TextBox_Dog_num_TextChanged(sender As Object, e As EventArgs) Handles TextBox_Dog_num.TextChanged
        OCC_UPDATE(LS)
        OCC_FAMILIA_SEARCH_EDIT(LS)
        UpdatePanel_Table_LS.Update()
        UpdatePanel_User_info.Update()
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Валидация поля "Общая площадь"----------------------------------------------------------------------------------------------------------------------------------------
    Protected Sub TextBox_SQ_TextChanged(sender As Object, e As EventArgs) Handles TextBox_SQ.TextChanged
        Dim chislo As Double
        Dim pattern As String = "^\d{1,10}\.{0,1}\d{0,10}$"
        If Regex.IsMatch(TextBox_SQ.Text, pattern, RegexOptions.IgnoreCase) Then
            Label_SQ_info.ForeColor = Drawing.Color.Blue
            Label_SQ_info.Text = "Изменено"
            chislo = Math.Round(Convert.ToDouble(TextBox_SQ.Text), 2)
            TextBox_SQ.Text = chislo.ToString()
            TextBox_SQ.ForeColor = Drawing.Color.Black
            OCC_UPDATE(LS)
            OCC_FAMILIA_SEARCH_EDIT(LS)
        Else
            Label_SQ_info.ForeColor = Drawing.Color.Red
            Label_SQ_info.Text = "Неверный формат числа!!!!!!!"
            TextBox_SQ.ForeColor = Drawing.Color.Red
        End If
        UpdatePanel_Table_LS.Update()
        UpdatePanel_User_info.Update()
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Валидация поля "Жилая площадь"----------------------------------------------------------------------------------------------------------------------------------------
    Protected Sub TextBox_SQ_live_TextChanged(sender As Object, e As EventArgs) Handles TextBox_SQ_live.TextChanged
        Dim chislo As Double
        Dim pattern As String = "^\d{1,10}\.{0,1}\d{0,10}$"
        If Regex.IsMatch(TextBox_SQ_live.Text, pattern, RegexOptions.IgnoreCase) Then
            Label_SQ_live_info.ForeColor = Drawing.Color.Blue
            Label_SQ_live_info.Text = "Изменено"
            chislo = Math.Round(Convert.ToDouble(TextBox_SQ_live.Text), 2)
            TextBox_SQ_live.Text = chislo.ToString()
            TextBox_SQ_live.ForeColor = Drawing.Color.Black
            OCC_UPDATE(LS)
            OCC_FAMILIA_SEARCH_EDIT(LS)
        Else
            Label_SQ_live_info.ForeColor = Drawing.Color.Red
            Label_SQ_live_info.Text = "Неверный формат числа!!!!!!!"
            TextBox_SQ_live.ForeColor = Drawing.Color.Red
        End If
        UpdatePanel_Table_LS.Update()
        UpdatePanel_User_info.Update()
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Валидация поля "Отапливаемая площадь"---------------------------------------------------------------------------------------------------------------------------------
    Protected Sub TextBox_SQ_hot_TextChanged(sender As Object, e As EventArgs) Handles TextBox_SQ_hot.TextChanged
        Dim chislo As Double
        Dim pattern As String = "^\d{1,10}\.{0,1}\d{0,10}$"
        If Regex.IsMatch(TextBox_SQ_hot.Text, pattern, RegexOptions.IgnoreCase) Then
            Label_SQ_hot_info.ForeColor = Drawing.Color.Blue
            Label_SQ_hot_info.Text = "Изменено"
            chislo = Math.Round(Convert.ToDouble(TextBox_SQ_hot.Text), 2)
            TextBox_SQ_hot.Text = chislo.ToString()
            TextBox_SQ_hot.ForeColor = Drawing.Color.Black
            OCC_UPDATE(LS)
            OCC_FAMILIA_SEARCH_EDIT(LS)
        Else
            Label_SQ_hot_info.ForeColor = Drawing.Color.Red
            Label_SQ_hot_info.Text = "Неверный формат числа!!!!!!!"
            TextBox_SQ_hot.ForeColor = Drawing.Color.Red
        End If
        UpdatePanel_Table_LS.Update()
        UpdatePanel_Detail.Update()
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Валидация поля "Площадь МОП"------------------------------------------------------------------------------------------------------------------------------------------
    Protected Sub TextBox_SQ_MOP_TextChanged(sender As Object, e As EventArgs) Handles TextBox_SQ_MOP.TextChanged
        Dim chislo As Double
        Dim pattern As String = "^\d{1,10}\.{0,1}\d{0,10}$"
        If Regex.IsMatch(TextBox_SQ_MOP.Text, pattern, RegexOptions.IgnoreCase) Then
            Label_SQ_MOP_info.ForeColor = Drawing.Color.Blue
            Label_SQ_MOP_info.Text = "Изменено"
            chislo = Math.Round(Convert.ToDouble(TextBox_SQ_MOP.Text), 2)
            TextBox_SQ_MOP.Text = chislo.ToString()
            TextBox_SQ_MOP.ForeColor = Drawing.Color.Black
            OCC_UPDATE(LS)
            OCC_FAMILIA_SEARCH_EDIT(LS)
        Else
            Label_SQ_MOP_info.ForeColor = Drawing.Color.Red
            Label_SQ_MOP_info.Text = "Неверный формат числа!!!!!!!"
            TextBox_SQ_MOP.ForeColor = Drawing.Color.Red
        End If
        UpdatePanel_Table_LS.Update()
        UpdatePanel_Detail.Update()
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Валидация поля "email"------------------------------------------------------------------------------------------------------------------------------------------------
    Protected Sub TextBox_Email_TextChanged(sender As Object, e As EventArgs) Handles TextBox_Email.TextChanged
        'Dim pattern As String = "^\w+\.?\w+@\w+\.?\w+$"
        'If Regex.IsMatch(TextBox_Email.Text, pattern, RegexOptions.IgnoreCase) Then
        '    Label_Email_info.ForeColor = Drawing.Color.Blue
        '    Label_Email_info.Text = "Изменено"
        'Else
        '    Label_Email_info.ForeColor = Drawing.Color.Red
        '    Label_Email_info.Text = "Неверный формат email!!!!!!!"
        '    TextBox_Email.Text = ""
        'End If
        Try
            If TextBox_Email.Text = "" Then
                Label_Email_info.ForeColor = Drawing.Color.Blue
                Label_Email_info.Text = "Изменено"
                TextBox_Email.ForeColor = Drawing.Color.Black
                OCC_UPDATE(LS)
                OCC_FAMILIA_SEARCH_EDIT(LS)
            Else
                Dim mail As System.Net.Mail.MailAddress = New System.Net.Mail.MailAddress(TextBox_Email.Text)
                Label_Email_info.ForeColor = Drawing.Color.Blue
                Label_Email_info.Text = "Изменено"
                TextBox_Email.ForeColor = Drawing.Color.Black
                OCC_UPDATE(LS)
                OCC_FAMILIA_SEARCH_EDIT(LS)
            End If

        Catch ex As Exception
            Label_Email_info.ForeColor = Drawing.Color.Red
            Label_Email_info.Text = "Неверный формат email!!!!!!!"
            TextBox_Email.ForeColor = Drawing.Color.Red
        End Try
        UpdatePanel_Table_LS.Update()
        UpdatePanel_Detail.Update()
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Валидация поля "Телефон"----------------------------------------------------------------------------------------------------------------------------------------------
    Protected Sub TextBox_Phone_TextChanged(sender As Object, e As EventArgs) Handles TextBox_Phone.TextChanged
        'If TextBox_Phone.Text.Length>
        Dim pattern As String = "^\d{11}$"
        If Regex.IsMatch(TextBox_Phone.Text, pattern, RegexOptions.IgnoreCase) Or TextBox_Phone.Text = "" Then
            Label_Phone_info.ForeColor = Drawing.Color.Blue
            Label_Phone_info.Text = "Изменено"
            TextBox_Phone.ForeColor = Drawing.Color.Blue
            OCC_UPDATE(LS)
            OCC_FAMILIA_SEARCH_EDIT(LS)
        Else
            Label_Phone_info.ForeColor = Drawing.Color.Red
            Label_Phone_info.Text = "Неверный формат номера телефона!!!!!!!"
            TextBox_Phone.ForeColor = Drawing.Color.Red
        End If
        UpdatePanel_Table_LS.Update()
        UpdatePanel_Detail.Update()
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Валидация поля "Телефон дополнительно"--------------------------------------------------------------------------------------------------------------------------------
    Protected Sub TextBox_Phone2_TextChanged(sender As Object, e As EventArgs) Handles TextBox_Phone2.TextChanged
        Dim pattern As String = "^\d{11}$"
        If Regex.IsMatch(TextBox_Phone2.Text, pattern, RegexOptions.IgnoreCase) Or TextBox_Phone2.Text = "" Then
            Label_Phone2_info.ForeColor = Drawing.Color.Blue
            Label_Phone2_info.Text = "Изменено"
            TextBox_Phone2.ForeColor = Drawing.Color.Blue
            OCC_UPDATE(LS)
            OCC_FAMILIA_SEARCH_EDIT(LS)
        Else
            Label_Phone2_info.ForeColor = Drawing.Color.Red
            Label_Phone2_info.Text = "Неверный формат номера телефона!!!!!!!"
            TextBox_Phone2.ForeColor = Drawing.Color.Red
        End If
        UpdatePanel_Table_LS.Update()
        UpdatePanel_Detail.Update()
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Редактирование поля "Закрытие"----------------------------------------------------------------------------------------------------------------------------------------
    Protected Sub DropDownList_Is_closed_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList_Is_closed.SelectedIndexChanged
        If DropDownList_Is_closed.Items.Count > 0 Then
            If DropDownList_Is_closed.SelectedIndex = 1 Then
                TextBox_Reason_arhiv.Text = ""
                'UpdatePanel_myModal_Arhiv_LS.Update()
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal_Arhiv_LS", "$('#myModal_Arhiv_LS').modal({backdrop: 'static',keyboard: false });", True)
            End If
        End If
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Редактирование поля "Тип Л/С"-----------------------------------------------------------------------------------------------------------------------------------------
    Protected Sub DropDownList_OCC_type_SelectedIndexChanged(sender As Object, e As EventArgs)
        OCC_UPDATE(LS)
        OCC_FAMILIA_SEARCH_EDIT(LS)
        UpdatePanel_Table_LS.Update()
        UpdatePanel_Detail.Update()
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Редактирование поля "Статус Л/С"--------------------------------------------------------------------------------------------------------------------------------------
    Protected Sub DropDownList_OCC_status_SelectedIndexChanged(sender As Object, e As EventArgs)
        OCC_UPDATE(LS)
        OCC_FAMILIA_SEARCH_EDIT(LS)
        UpdatePanel_Table_LS.Update()
        UpdatePanel_Detail.Update()
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Редактирование поля "Свойство Л/С"------------------------------------------------------------------------------------------------------------------------------------
    Protected Sub DropDownList_OCC_property_SelectedIndexChanged(sender As Object, e As EventArgs)
        OCC_UPDATE(LS)
        OCC_FAMILIA_SEARCH_EDIT(LS)
        UpdatePanel_Table_LS.Update()
        UpdatePanel_Detail.Update()
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Валидация поля "Кадастр"----------------------------------------------------------------------------------------------------------------------------------------------
    Protected Sub TextBox_Kadastr_TextChanged(sender As Object, e As EventArgs) Handles TextBox_Kadastr.TextChanged
        OCC_UPDATE(LS)
        OCC_FAMILIA_SEARCH_EDIT(LS)
        UpdatePanel_Table_LS.Update()
        UpdatePanel_Detail.Update()
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Валидация поля "ГИС"--------------------------------------------------------------------------------------------------------------------------------------------------
    Protected Sub TextBox_GIS_TextChanged(sender As Object, e As EventArgs) Handles TextBox_GIS.TextChanged
        OCC_UPDATE(LS)
        OCC_FAMILIA_SEARCH_EDIT(LS)
        UpdatePanel_Table_LS.Update()
        UpdatePanel_Detail.Update()
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Валидация поля "Код"--------------------------------------------------------------------------------------------------------------------------------------------------
    Protected Sub TextBox_Code_TextChanged(sender As Object, e As EventArgs) Handles TextBox_Code.TextChanged
        OCC_UPDATE(LS)
        OCC_FAMILIA_SEARCH_EDIT(LS)
        UpdatePanel_Table_LS.Update()
        UpdatePanel_Detail.Update()
    End Sub
    '                                                                                                                                                                                              |
    '***********************************************************************************************************************************************************************************************



    '***********************************************************************************************************************************************************************************************
    '|||||||||||||||||||||||| ВКЛАДКА "ЛЮДИ" |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    '***********************************************************************************************************************************************************************************************
    '                                                                                                                                                                                              |
    '-------------------------Функция (Заполнение грида "Люди")-------------------------------------------------------------------------------------------------------------------------------------
    Protected Sub OCC_FAMILIA_SEARCH_EDIT_USER(LS As Int64)
        Label_User_info_header.Text = "Л/С №" & LS.ToString() & " адрес " & ADDR
        Dim MyConnection As SqlConnection
        Dim MyDataAdapter As SqlDataAdapter
        Dim con1 As String
        Dim s1 As String = FormsAuthentication.Decrypt(Request.Cookies("AppBillingCookie").Value).UserData
        con1 = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
        MyConnection = New SqlConnection(con1)
        MyDataAdapter = New SqlDataAdapter("APP_OCC_LS_FAMILIA_USER", MyConnection)
        MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@LS", SqlDbType.BigInt))
        MyDataAdapter.SelectCommand.Parameters("@LS").Value = LS
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@OPER_GUID", SqlDbType.VarChar, 50))
        MyDataAdapter.SelectCommand.Parameters("@OPER_GUID").Value = s1
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@APP_GUID", SqlDbType.VarChar, 50))
        MyDataAdapter.SelectCommand.Parameters("@APP_GUID").Value = ConfigurationManager.AppSettings.Item("app_guid").ToString
        '---------------------------------------------------------------------------------------
        Dim DS As DataSet
        DS = New DataSet()
        MyDataAdapter.Fill(DS)
        GridView_User_info.DataSource = DS.Tables(0).DefaultView
        GridView_User_info.AutoGenerateColumns = False
        GridView_User_info.Visible = True
        GridView_User_info.DataBind()
        Dim dv1 As DataView = New DataView(DS.Tables(1))
        Dim dv2 As DataView = New DataView(DS.Tables(2))
        Dim dv3 As DataView = New DataView(DS.Tables(3))
        For i As Integer = 0 To GridView_User_info.Rows.Count - 1
            Dim Label_User_Sex_Item As Label = GridView_User_info.Rows(i).FindControl("Label_User_Sex_Item")
            If IsNothing(Label_User_Sex_Item) Then
            Else
                Label_User_Sex_Item.Text = DS.Tables(1).Rows.Item(DS.Tables(0).Rows.Item(i).Item(7)).Item(1)
            End If
            Dim DropDownList_User_Sex_Edit As DropDownList = GridView_User_info.Rows(i).FindControl("DropDownList_User_Sex_Edit")
            If IsNothing(DropDownList_User_Sex_Edit) Then
            Else
                DropDownList_User_Sex_Edit.DataSource = dv1
                DropDownList_User_Sex_Edit.DataTextField = "S"
                DropDownList_User_Sex_Edit.DataValueField = "ID_S"
                DropDownList_User_Sex_Edit.DataBind()
                DropDownList_User_Sex_Edit.SelectedValue = DS.Tables(0).Rows.Item(i).Item(7)
            End If
            Dim DropDownList_User_Reg_Edit As DropDownList = GridView_User_info.Rows(i).FindControl("DropDownList_User_Reg_Edit")
            If IsNothing(DropDownList_User_Reg_Edit) Then
            Else
                DropDownList_User_Reg_Edit.DataSource = dv2
                DropDownList_User_Reg_Edit.DataTextField = "Type"
                DropDownList_User_Reg_Edit.DataValueField = "ID_reg_type"
                DropDownList_User_Reg_Edit.DataBind()
                DropDownList_User_Reg_Edit.SelectedValue = DS.Tables(0).Rows.Item(i).Item(9)
            End If
            Dim DropDownList_User_Relation_Edit As DropDownList = GridView_User_info.Rows(i).FindControl("DropDownList_User_Relation_Edit")
            If IsNothing(DropDownList_User_Relation_Edit) Then
            Else
                DropDownList_User_Relation_Edit.DataSource = dv3
                DropDownList_User_Relation_Edit.DataTextField = "WHO"
                DropDownList_User_Relation_Edit.DataValueField = "ID"
                DropDownList_User_Relation_Edit.DataBind()
                DropDownList_User_Relation_Edit.SelectedValue = DS.Tables(0).Rows.Item(i).Item(8)
            End If
            If DS.Tables(0).Rows.Item(i).Item(8) = "отвл" Then
                GridView_User_info.Rows.Item(i).BackColor = Drawing.Color.Yellow
                If GridView_User_info.EditIndex = -1 Then
                    GridView_User_info.Rows(i).FindControl("ImageButton_User_Unreg").Visible = False
                End If
            End If
            If DS.Tables(0).Rows.Item(i).Item(12).ToString().Length = 4 Then
                GridView_User_info.Rows.Item(i).BackColor = Drawing.Color.LightGray
                GridView_User_info.Rows.Item(i).Enabled = False
                If GridView_User_info.EditIndex = -1 Then
                    GridView_User_info.Rows(i).FindControl("ImageButton_User_Unreg").Visible = False
                    GridView_User_info.Rows(i).FindControl("ImageButton_User_Edit").Visible = False
                End If
            End If
            If Not ((DS.Tables(0).Rows.Item(i).Item(8) = "отвл") Or (DS.Tables(0).Rows.Item(i).Item(12).ToString().Length = 4)) Then
                GridView_User_info.Rows.Item(i).BackColor = Drawing.Color.White
                'GridView_User_info.Rows(i).FindControl("ImageButton_User_Unreg").Visible = True
            End If
            If GridView_User_info.EditIndex > -1 Then
                If GridView_User_info.EditIndex = i Then
                    DropDownList_User_Reg_Edit = GridView_User_info.Rows(GridView_User_info.EditIndex).FindControl("DropDownList_User_Reg_Edit")
                    If (DS.Tables(0).Rows.Item(i).Item(9) = "врем") Then
                        Dim TextBox_User_EndReg_Edit As TextBox = GridView_User_info.Rows(GridView_User_info.EditIndex).FindControl("TextBox_User_EndReg_Edit")
                        'TextBox_User_EndReg_Edit.ReadOnly = False
                        'GridView_User_info.Rows.Item(i).BackColor = Drawing.Color.White
                    Else
                        Dim TextBox_User_EndReg_Edit As TextBox = GridView_User_info.Rows(GridView_User_info.EditIndex).FindControl("TextBox_User_EndReg_Edit")
                        'TextBox_User_EndReg_Edit.ReadOnly = True
                        'GridView_User_info.Rows.Item(i).BackColor = Drawing.Color.White
                    End If
                End If
            End If
            If ((DS.Tables(0).Rows.Item(i).Item(9) = "врем") And (GridView_User_info.EditIndex <= -1)) Then
                Dim Label_User_EndReg_Item As Label = GridView_User_info.Rows(i).FindControl("Label_User_EndReg_Item")
                If Convert.ToDateTime(Label_User_EndReg_Item.Text) > Date.Now Then
                    GridView_User_info.Rows.Item(i).BackColor = Drawing.Color.LightGreen
                Else
                    GridView_User_info.Rows.Item(i).BackColor = Drawing.Color.Red
                End If
            End If
        Next
        UpdatePanel_User_info.Update()
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Функция "РЕДАКТИРОВАНИЕ, РЕГИСТРАЦИЯ, ВЫПИСКА ЛЮДЕЙ"------------------------------------------------------------------------------------------------------------------
    Protected Sub USER_UPDATE(USER_GUID As Guid, type As Integer)
        Dim MyConnection As SqlConnection
        Dim MySqlComand As SqlCommand
        Dim con1 As String
        Dim s1 As String = FormsAuthentication.Decrypt(Request.Cookies("AppBillingCookie").Value).UserData
        con1 = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
        MyConnection = New SqlConnection(con1)
        MySqlComand = New SqlCommand("APP_Users_Edit", MyConnection)
        MySqlComand.CommandType = CommandType.StoredProcedure
        '---------------------------------Редактирование человека-----------------------------------
        If type = 1 Then
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@type", SqlDbType.Int))
            MySqlComand.Parameters("@type").Value = 1
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@GUID_user", SqlDbType.UniqueIdentifier))
            MySqlComand.Parameters("@GUID_user").Value = USER_GUID
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@OPER_GUID", SqlDbType.VarChar, 50))
            MySqlComand.Parameters("@OPER_GUID").Value = s1
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@APP_GUID", SqlDbType.VarChar, 50))
            MySqlComand.Parameters("@APP_GUID").Value = ConfigurationManager.AppSettings.Item("app_guid").ToString
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@HOST", SqlDbType.VarChar, 50))
            MySqlComand.Parameters("@HOST").Value = HttpContext.Current.Request.UserHostAddress
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@Surname", SqlDbType.VarChar, 50))
            Dim TextBox_Familia_Serch_2_User_Surname_Edit As TextBox = GridView_User_info.Rows(GridView_User_info.EditIndex).FindControl("TextBox_Familia_Serch_2_User_Surname_Edit")
            MySqlComand.Parameters("@Surname").Value = TextBox_Familia_Serch_2_User_Surname_Edit.Text
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@Name", SqlDbType.VarChar, 50))
            Dim TextBox_Familia_Serch_2_User_Name_Edit As TextBox = GridView_User_info.Rows(GridView_User_info.EditIndex).FindControl("TextBox_Familia_Serch_2_User_Name_Edit")
            MySqlComand.Parameters("@Name").Value = TextBox_Familia_Serch_2_User_Name_Edit.Text
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@Patronymic", SqlDbType.VarChar, 50))
            Dim TextBox_Familia_Serch_2_User_Patronymic_Edit As TextBox = GridView_User_info.Rows(GridView_User_info.EditIndex).FindControl("TextBox_Familia_Serch_2_User_Patronymic_Edit")
            MySqlComand.Parameters("@Patronymic").Value = TextBox_Familia_Serch_2_User_Patronymic_Edit.Text
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@Date_birth", SqlDbType.Date))
            Dim TextBox_Familia_Serch_2_User_Datebirth_Edit As TextBox = GridView_User_info.Rows(GridView_User_info.EditIndex).FindControl("TextBox_Familia_Serch_2_User_Datebirth_Edit")
            Dim d_birth As String = Convert.ToDateTime(TextBox_Familia_Serch_2_User_Datebirth_Edit.Text).ToString("yyyy-MM-dd")
            MySqlComand.Parameters("@Date_birth").Value = d_birth ' DateTime.ParseExact(d_birth, "yyyy-MM-dd", Nothing)
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@Sex", SqlDbType.Int))
            Dim DropDownList_User_Sex_Edit As DropDownList = GridView_User_info.Rows(GridView_User_info.EditIndex).FindControl("DropDownList_User_Sex_Edit")
            MySqlComand.Parameters("@Sex").Value = DropDownList_User_Sex_Edit.SelectedValue
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@WHO_ID", SqlDbType.Char, 4))
            Dim DropDownList_User_Relation_Edit As DropDownList = GridView_User_info.Rows(GridView_User_info.EditIndex).FindControl("DropDownList_User_Relation_Edit")
            MySqlComand.Parameters("@WHO_ID").Value = DropDownList_User_Relation_Edit.SelectedValue
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@ID_registration_type", SqlDbType.Char, 4))
            Dim DropDownList_User_Reg_Edit As DropDownList = GridView_User_info.Rows(GridView_User_info.EditIndex).FindControl("DropDownList_User_Reg_Edit")
            MySqlComand.Parameters("@ID_registration_type").Value = DropDownList_User_Reg_Edit.SelectedValue
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@Date_registration", SqlDbType.Date))
            Dim TextBox_Familia_Serch_2_User_DateReg_Edit As TextBox = GridView_User_info.Rows(GridView_User_info.EditIndex).FindControl("TextBox_Familia_Serch_2_User_DateReg_Edit")
            Dim d_reg As String = Convert.ToDateTime(TextBox_Familia_Serch_2_User_DateReg_Edit.Text).ToString("yyyy-MM-dd")
            MySqlComand.Parameters("@Date_registration").Value = d_reg ' DateTime.ParseExact(d_reg, "yyyy-MM-dd", Nothing)
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@FT_REG", SqlDbType.Date))
            MySqlComand.Parameters("@FT_REG").Value = Date.Now.Date
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@STATUS_UNREG", SqlDbType.Char, 4))
            MySqlComand.Parameters("@STATUS_UNREG").Value = DBNull.Value
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@Date_registration_end", SqlDbType.Date))
            If DropDownList_User_Reg_Edit.SelectedValue = "врем" Then
                Dim TextBox_User_EndReg_Edit As TextBox = GridView_User_info.Rows(GridView_User_info.EditIndex).FindControl("TextBox_User_EndReg_Edit")
                Dim d_unreg As Date
                If Date.TryParse(TextBox_User_EndReg_Edit.Text, d_unreg) Then
                    d_unreg = Convert.ToDateTime(TextBox_User_EndReg_Edit.Text).ToString("yyyy-MM-dd")
                    MySqlComand.Parameters("@Date_registration_end").Value = d_unreg
                End If
            Else
                MySqlComand.Parameters("@Date_registration_end").Value = DBNull.Value
            End If
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@FT_UNREG", SqlDbType.Date))
            MySqlComand.Parameters("@FT_UNREG").Value = DBNull.Value 'Date.Now.Date
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@ARH", SqlDbType.Int))
            MySqlComand.Parameters("@ARH").Value = 0
            '---------------------------------------------------------------------------------------
        End If
        '---------------------------------Регистрация человека--------------------------------------
        If type = 3 Then
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@type", SqlDbType.Int))
            MySqlComand.Parameters("@type").Value = 3
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@LS", SqlDbType.Int))
            MySqlComand.Parameters("@LS").Value = LS
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@Surname", SqlDbType.VarChar, 50))
            MySqlComand.Parameters("@Surname").Value = TextBox_Add_User_Surname.Text
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@Name", SqlDbType.VarChar, 50))
            MySqlComand.Parameters("@Name").Value = TextBox_Add_User_Name.Text
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@Patronymic", SqlDbType.VarChar, 50))
            MySqlComand.Parameters("@Patronymic").Value = TextBox_Add_User_Patronymic.Text
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@Date_birth", SqlDbType.Date))
            Dim d_birth As String = Convert.ToDateTime(TextBox_User_Add_Date_Birth.Text).ToString("yyyy-MM-dd")
            MySqlComand.Parameters("@Date_birth").Value = d_birth ' DateTime.ParseExact(d_birth, "yyyy-MM-dd", Nothing)
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@Sex", SqlDbType.Int))
            MySqlComand.Parameters("@Sex").Value = DropDownList_User_Add_Sex.SelectedValue
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@WHO_ID", SqlDbType.Char, 4))
            MySqlComand.Parameters("@WHO_ID").Value = DropDownList_User_Add_Who_ID.SelectedValue
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@ID_registration_type", SqlDbType.Char, 4))
            MySqlComand.Parameters("@ID_registration_type").Value = DropDownList_User_Add_Reg_Type.SelectedValue
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@Date_registration", SqlDbType.Date))
            MySqlComand.Parameters("@Date_registration").Value = Convert.ToDateTime(TextBox_User_Add_Date_Reg.Text).ToString("yyyy-MM-dd")
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@FT_REG", SqlDbType.Date))
            MySqlComand.Parameters("@FT_REG").Value = Convert.ToDateTime(TextBox_User_Add_Date_Reg.Text).ToString("yyyy-MM-dd")
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@Date_registration_end", SqlDbType.Date))
            MySqlComand.Parameters("@Date_registration_end").Value = Convert.ToDateTime(TextBox_User_Add_End_Reg.Text).ToString("yyyy-MM-dd")
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@OPER_GUID", SqlDbType.VarChar, 50))
            MySqlComand.Parameters("@OPER_GUID").Value = s1
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@APP_GUID", SqlDbType.VarChar, 50))
            MySqlComand.Parameters("@APP_GUID").Value = ConfigurationManager.AppSettings.Item("app_guid").ToString
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@HOST", SqlDbType.VarChar, 50))
            MySqlComand.Parameters("@HOST").Value = HttpContext.Current.Request.UserHostAddress
            '---------------------------------------------------------------------------------------
        End If
        '-------------------------------Выписка человека--------------------------------------------
        If type = 4 Then
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@type", SqlDbType.Int))
            MySqlComand.Parameters("@type").Value = 4
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@GUID_user", SqlDbType.UniqueIdentifier))
            MySqlComand.Parameters("@GUID_user").Value = USER_GUID
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@OPER_GUID", SqlDbType.VarChar, 50))
            MySqlComand.Parameters("@OPER_GUID").Value = s1
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@APP_GUID", SqlDbType.VarChar, 50))
            MySqlComand.Parameters("@APP_GUID").Value = ConfigurationManager.AppSettings.Item("app_guid").ToString
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@HOST", SqlDbType.VarChar, 50))
            MySqlComand.Parameters("@HOST").Value = HttpContext.Current.Request.UserHostAddress
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@Date_registration_end", SqlDbType.Date))
            MySqlComand.Parameters("@Date_registration_end").Value = Convert.ToDateTime(TextBox_Date_Unreg.Text).ToString("yyyy-MM-dd")
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@FT_UNREG", SqlDbType.Date))
            MySqlComand.Parameters("@FT_UNREG").Value = Convert.ToDateTime(TextBox_Date_Unreg.Text).ToString("yyyy-MM-dd")
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@STATUS_UNREG", SqlDbType.Char, 4))
            MySqlComand.Parameters("@STATUS_UNREG").Value = DropDownList_Unreg_status.SelectedValue
            '---------------------------------------------------------------------------------------
        End If
        MyConnection.Open()
        MySqlComand.ExecuteNonQuery()
        MyConnection.Close()
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Клик по картинке "Редактировать" в гриде "Люди"-----------------------------------------------------------------------------------------------------------------------
    Protected Sub GridView_User_info_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles GridView_User_info.RowEditing
        GridView_User_info.EditIndex = e.NewEditIndex
        'Dim ID_user_doc As Int32 = Convert.ToInt64(GridView_Doc.DataKeys(GridView_Doc.EditIndex).Value)
        OCC_FAMILIA_SEARCH_EDIT_USER(LS)
        Dim MyConnection As SqlConnection
        Dim MyDataAdapter As SqlDataAdapter
        Dim con1 As String
        Dim s1 As String = FormsAuthentication.Decrypt(Request.Cookies("AppBillingCookie").Value).UserData
        con1 = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
        MyConnection = New SqlConnection(con1)
        MyDataAdapter = New SqlDataAdapter("APP_OCC_LS_FAMILIA_USER", MyConnection)
        MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@LS", SqlDbType.BigInt))
        MyDataAdapter.SelectCommand.Parameters("@LS").Value = LS
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@OPER_GUID", SqlDbType.VarChar, 50))
        MyDataAdapter.SelectCommand.Parameters("@OPER_GUID").Value = s1
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@APP_GUID", SqlDbType.VarChar, 50))
        MyDataAdapter.SelectCommand.Parameters("@APP_GUID").Value = ConfigurationManager.AppSettings.Item("app_guid").ToString
        '---------------------------------------------------------------------------------------
        Dim DS As DataSet
        DS = New DataSet()
        MyDataAdapter.Fill(DS)
        Dim dv1 As DataView = New DataView(DS.Tables(1))
        Dim DropDownList_User_Sex_Edit As DropDownList = GridView_User_info.Rows(e.NewEditIndex).FindControl("DropDownList_User_Sex_Edit")
        DropDownList_User_Sex_Edit.DataSource = dv1
        DropDownList_User_Sex_Edit.DataTextField = "S"
        DropDownList_User_Sex_Edit.DataValueField = "ID_S"
        DropDownList_User_Sex_Edit.DataBind()
        DropDownList_User_Sex_Edit.SelectedValue = DS.Tables(0).Rows.Item(0).Item(5)
        Dim dv2 As DataView = New DataView(DS.Tables(2))
        Dim DropDownList_User_Reg_Edit As DropDownList = GridView_User_info.Rows(e.NewEditIndex).FindControl("DropDownList_User_Reg_Edit")
        DropDownList_User_Reg_Edit.DataSource = dv2
        DropDownList_User_Reg_Edit.DataTextField = "Type"
        DropDownList_User_Reg_Edit.DataValueField = "ID_reg_type"
        DropDownList_User_Reg_Edit.DataBind()
        DropDownList_User_Reg_Edit.SelectedValue = DS.Tables(0).Rows.Item(0).Item(6)
        UpdatePanel_User_info.Update()
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Клик по картинке "Сохранить" в гриде "Люди"---------------------------------------------------------------------------------------------------------------------------
    Protected Sub GridView_User_info_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles GridView_User_info.RowUpdating
        Try
            Label_User_Update_Error.Text = ""
            USER_UPDATE(GridView_User_info.DataKeys.Item(GridView_User_info.EditIndex).Value, 1)
            GridView_User_info.EditIndex = -1
            OCC_LS(LS, LS)
            UpdatePanel_LS.Update()
            OCC_FAMILIA_SEARCH_EDIT_USER(LS)
            UpdatePanel_User_info.Update()
        Catch ex As Exception
            Label_User_Update_Error.ForeColor = Drawing.Color.Red
            Label_User_Update_Error.Text = "!!!Ошибка!!! " + ex.Message
            UpdatePanel_User_info.Update()
            e.Cancel = True
        End Try


    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Клик по картинке "Отменить" в гриде "Люди"----------------------------------------------------------------------------------------------------------------------------
    Protected Sub GridView_Familia_Serch_2_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles GridView_User_info.RowCancelingEdit
        Label_User_Update_Error.Text = ""
        GridView_User_info.EditIndex = -1
        OCC_FAMILIA_SEARCH_EDIT_USER(LS)
        UpdatePanel_User_info.Update()
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Выбор типа прописки при регистрации нового человека-------------------------------------------------------------------------------------------------------------------
    Protected Sub DropDownList_User_Add_Reg_Type_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList_User_Add_Reg_Type.SelectedIndexChanged
        If DropDownList_User_Add_Reg_Type.SelectedValue = "врем" Then
            TextBox_User_Add_End_Reg.Visible = True
            UpdatePanel_User_Add.Update()
        Else
            TextBox_User_Add_End_Reg.Visible = False
            UpdatePanel_User_Add.Update()
        End If
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Выбор типа прописки при редактировании человека-----------------------------------------------------------------------------------------------------------------------
    Protected Sub DropDownList_User_Reg_Edit_SelectedIndexChanged(sender As Object, e As EventArgs) ' Handles DropDownList_User_Reg_Edit.SelectedIndexChanged
        '    If DropDownList_User_Reg_Edit.SelectedValue = "врем" Then
        '        'TextBox_User_EndReg_Edit.
        '        MsgBox("Временная прописка")
        '    End If
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Клик по картинке "Выписать" грида "Люди" (вызов модального окна "Выписка человека")-----------------------------------------------------------------------------------
    Protected Sub GridView_User_info_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles GridView_User_info.RowDeleting
        Dim MyConnection As SqlConnection
        Dim MyDataAdapter As SqlDataAdapter
        Dim con1 As String
        Dim s1 As String = FormsAuthentication.Decrypt(Request.Cookies("AppBillingCookie").Value).UserData
        con1 = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
        MyConnection = New SqlConnection(con1)
        MyDataAdapter = New SqlDataAdapter("APP_USER_COMBO", MyConnection)
        MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@OPER_GUID", SqlDbType.VarChar, 50))
        MyDataAdapter.SelectCommand.Parameters("@OPER_GUID").Value = s1
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@APP_GUID", SqlDbType.VarChar, 50))
        MyDataAdapter.SelectCommand.Parameters("@APP_GUID").Value = ConfigurationManager.AppSettings.Item("app_guid").ToString
        '---------------------------------------------------------------------------------------
        Dim DS As DataSet
        DS = New DataSet()
        MyDataAdapter.Fill(DS)
        Dim dv2 As DataView = New DataView(DS.Tables(2))

        DropDownList_Unreg_status.DataSource = dv2
        DropDownList_Unreg_status.DataTextField = "Type"
        DropDownList_Unreg_status.DataValueField = "ID_reg_type"
        DropDownList_Unreg_status.DataBind()
        DropDownList_Unreg_status.SelectedValue = "????"

        UpdatePanel_User_Unreg.Update()
        GUID_user = GridView_User_info.DataKeys.Item(e.RowIndex).Value
        index_unreg = e.RowIndex
        UpdatePanel_User_info.Update()
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal_delete_user", "$('#myModal_delete_user').modal({backdrop: 'static',keyboard: false });", True)
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Нажатие на кнопку "Регистрация" (вызов модального окна "Регистрация человека")----------------------------------------------------------------------------------------
    Protected Sub Button_Reg_User_Click(sender As Object, e As EventArgs) Handles Button_Reg_User.Click
        Dim MyConnection As SqlConnection
        Dim MyDataAdapter As SqlDataAdapter
        Dim con1 As String
        Dim s1 As String = FormsAuthentication.Decrypt(Request.Cookies("AppBillingCookie").Value).UserData
        con1 = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
        MyConnection = New SqlConnection(con1)
        MyDataAdapter = New SqlDataAdapter("APP_USER_COMBO", MyConnection)
        MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@OPER_GUID", SqlDbType.VarChar, 50))
        MyDataAdapter.SelectCommand.Parameters("@OPER_GUID").Value = s1
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@APP_GUID", SqlDbType.VarChar, 50))
        MyDataAdapter.SelectCommand.Parameters("@APP_GUID").Value = ConfigurationManager.AppSettings.Item("app_guid").ToString
        '---------------------------------------------------------------------------------------
        Dim DS As DataSet
        DS = New DataSet()
        MyDataAdapter.Fill(DS)
        Dim dv0 As DataView = New DataView(DS.Tables(0))
        Dim dv1 As DataView = New DataView(DS.Tables(1))
        Dim dv2 As DataView = New DataView(DS.Tables(2))
        Dim dv3 As DataView = New DataView(DS.Tables(3))

        DropDownList_User_Add_Sex.DataSource = dv0
        DropDownList_User_Add_Sex.DataTextField = "S"
        DropDownList_User_Add_Sex.DataValueField = "ID_S"
        DropDownList_User_Add_Sex.DataBind()
        DropDownList_User_Add_Sex.SelectedValue = 0

        DropDownList_User_Add_Reg_Type.DataSource = dv1
        DropDownList_User_Add_Reg_Type.DataTextField = "Type"
        DropDownList_User_Add_Reg_Type.DataValueField = "ID_reg_type"
        DropDownList_User_Add_Reg_Type.DataBind()
        DropDownList_User_Add_Reg_Type.SelectedValue = "пост"

        DropDownList_User_Add_Who_ID.DataSource = dv3
        DropDownList_User_Add_Who_ID.DataTextField = "WHO"
        DropDownList_User_Add_Who_ID.DataValueField = "ID"
        DropDownList_User_Add_Who_ID.DataBind()
        DropDownList_User_Add_Who_ID.SelectedValue = "????"

        UpdatePanel_User_Add.Update()
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal_Add_User", "$('#myModal_Add_User').modal({backdrop: 'static',keyboard: false });", True)
        'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalSEARCH_OCC_FAMILIA", "$('#myModalSEARCH_OCC_FAMILIA').modal({backdrop: 'static',keyboard: false });", True)
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Нажатие на кнопку "Зарегистрировать" модального окна "Регистрация человека"-------------------------------------------------------------------------------------------
    Protected Sub Button_User_Add_Click(sender As Object, e As EventArgs)
        Try
            USER_UPDATE(Nothing, 3)
            OCC_FAMILIA_SEARCH_EDIT_USER(LS)
            UpdatePanel_User_info.Update()
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal_Add_User", "$('#myModal_Add_User').modal('hide');", True)
        Catch ex As Exception
            Label_User_Add_Error1.Text = "Ошибка"
            Label_User_Add_Error2.ForeColor = Drawing.Color.Blue
            Label_User_Add_Error2.Text = ex.Message
            UpdatePanel_User_Add.Update()
        End Try
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Нажатие на кнопку "Выписать" модального окна "Delete_User"------------------------------------------------------------------------------------------------------------
    Protected Sub Button_Unreg_Click(sender As Object, e As EventArgs) ' Handles Button_myModal_Arhiv_LS.Click
        'USER_UPDATE(GridView_User_info.DataKeys.Item(GridView_User_info.SelectedIndex).Value)
        Try
            USER_UPDATE(GUID_user, 4)
            OCC_FAMILIA_SEARCH_EDIT_USER(LS)
            UpdatePanel_User_info.Update()
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal_delete_user", "$('#myModal_delete_user').modal('hide');", True)
        Catch ex As Exception
            Label_User_Unreg_Error_1.Text = "Ошибка"
            Label_User_Unreg_Error_2.ForeColor = Drawing.Color.Blue
            Label_User_Unreg_Error_2.Text = ex.Message
            UpdatePanel_User_Unreg.Update()
        End Try
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Клик по картинке "Документы" в гриде вкладки "Люди" ------------------------------------------------------------------------------------------------------------------
    Protected Sub GridView_User_info_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView_User_info.RowCommand

        If (e.CommandName = "edit_click") Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            GUID_user = GridView_User_info.DataKeys(index).Value
            OCC_FAMILIA_SEARCH_EDIT_USER_DOC(GUID_user)
            Button_Doc_Add.Visible = True
        End If
        'If (e.CommandName = "Unreg") Then
        '    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal_delete_user", "$('#myModal_delete_user').modal({backdrop: 'static',keyboard: false });", True)

        'End If
    End Sub
    '                                                                                                                                                                                              |
    '***********************************************************************************************************************************************************************************************



    '***********************************************************************************************************************************************************************************************
    '|||||||||||||||||||||||| ВКЛАДКА "ЛЮДИ/ДОКУМЕНТЫ" |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    '***********************************************************************************************************************************************************************************************
    '                                                                                                                                                                                              |
    '-------------------------Функция (Заполнение грида "Документы" на вкладке "Люди")--------------------------------------------------------------------------------------------------------------
    Protected Sub OCC_FAMILIA_SEARCH_EDIT_USER_DOC(GUID_user As Guid)
        Dim MyConnection As SqlConnection
        Dim MyDataAdapter As SqlDataAdapter
        Dim con1 As String
        Dim s1 As String = FormsAuthentication.Decrypt(Request.Cookies("AppBillingCookie").Value).UserData
        con1 = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
        MyConnection = New SqlConnection(con1)
        MyDataAdapter = New SqlDataAdapter("APP_OCC_LS_FAMILIA_USER_DOC", MyConnection)
        MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@GUID_user", SqlDbType.UniqueIdentifier))
        MyDataAdapter.SelectCommand.Parameters("@GUID_user").Value = GUID_user
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@OPER_GUID", SqlDbType.VarChar, 50))
        MyDataAdapter.SelectCommand.Parameters("@OPER_GUID").Value = s1
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@APP_GUID", SqlDbType.VarChar, 50))
        MyDataAdapter.SelectCommand.Parameters("@APP_GUID").Value = ConfigurationManager.AppSettings.Item("app_guid").ToString
        '---------------------------------------------------------------------------------------
        Dim DS As DataSet
        DS = New DataSet()
        MyDataAdapter.Fill(DS)
        'SqlDataSourceUserDocs.SelectParameters.Add("@GUID_user", GUID_user.ToString())
        'SqlDataSourceUserDocs.SelectParameters.Add("@OPER_GUID", s1)
        'SqlDataSourceUserDocs.SelectParameters.Add("@APP_GUID", ConfigurationManager.AppSettings.Item("app_guid").ToString)
        GridView_Doc.DataSource = DS.Tables(0).DefaultView
        GridView_Doc.AutoGenerateColumns = False
        GridView_Doc.Visible = True
        GridView_Doc.DataBind()

        Dim dv4 As DataView = New DataView(DS.Tables(1))
        For i As Integer = 0 To GridView_Doc.Rows.Count - 1
            Dim DropDownList_Doc_Type_Edit As DropDownList = GridView_Doc.Rows(i).FindControl("DropDownList_Doc_Type_Edit")
            If IsNothing(DropDownList_Doc_Type_Edit) Then
            Else
                DropDownList_Doc_Type_Edit.DataSource = dv4
                DropDownList_Doc_Type_Edit.DataTextField = "Type"
                DropDownList_Doc_Type_Edit.DataValueField = "Id_doc_type"
                DropDownList_Doc_Type_Edit.DataBind()
                DropDownList_Doc_Type_Edit.SelectedValue = DS.Tables(0).Rows.Item(i).Item(7)
            End If
        Next
        UpdatePanel_User_info.Update()
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Функция "РЕДАКТИРОВАНИЕ, ДОБАВЛЕНИЕ, АРХИВАЦИЯ ДОКУМЕНТОВ"------------------------------------------------------------------------------------------------------------
    Protected Sub USER_DOC_UPDATE(USER_DOC_ID As Integer, type As Integer)
        'Try
        Dim MyConnection As SqlConnection
        Dim MySqlComand As SqlCommand
        Dim con1 As String
        Dim s1 As String = FormsAuthentication.Decrypt(Request.Cookies("AppBillingCookie").Value).UserData
        con1 = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
        MyConnection = New SqlConnection(con1)
        MySqlComand = New SqlCommand("APP_USER_DOC_EDIT", MyConnection)
        MySqlComand.CommandType = CommandType.StoredProcedure
        '---------------------------------Редактирование документа-----------------------------------
        If type = 1 Then
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@type", SqlDbType.Int))
            MySqlComand.Parameters("@type").Value = 1
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@User_GUID", SqlDbType.UniqueIdentifier))
            MySqlComand.Parameters("@User_GUID").Value = GUID_user
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@ID_User_doc", SqlDbType.Int))
            MySqlComand.Parameters("@ID_User_doc").Value = USER_DOC_ID
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@OPER_GUID", SqlDbType.VarChar, 50))
            MySqlComand.Parameters("@OPER_GUID").Value = s1
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@APP_GUID", SqlDbType.VarChar, 50))
            MySqlComand.Parameters("@APP_GUID").Value = ConfigurationManager.AppSettings.Item("app_guid").ToString
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@HOST", SqlDbType.VarChar, 50))
            MySqlComand.Parameters("@HOST").Value = HttpContext.Current.Request.UserHostAddress
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@Type_doc", SqlDbType.Int))
            Dim DropDownList_Doc_Type_Edit As DropDownList = GridView_Doc.Rows(GridView_Doc.EditIndex).FindControl("DropDownList_Doc_Type_Edit")
            MySqlComand.Parameters("@Type_doc").Value = DropDownList_Doc_Type_Edit.SelectedValue
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@Series", SqlDbType.VarChar, 50))
            Dim TextBox_Series_Edit As TextBox = GridView_Doc.Rows(GridView_Doc.EditIndex).FindControl("TextBox_Series_Edit")
            MySqlComand.Parameters("@Series").Value = TextBox_Series_Edit.Text
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@Number", SqlDbType.VarChar, 50))
            Dim TextBox_Number_Edit As TextBox = GridView_Doc.Rows(GridView_Doc.EditIndex).FindControl("TextBox_Number_Edit")
            MySqlComand.Parameters("@Number").Value = TextBox_Number_Edit.Text
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@Date", SqlDbType.Date))
            Dim TextBox_Doc_Date_Edit As TextBox = GridView_Doc.Rows(GridView_Doc.EditIndex).FindControl("TextBox_Doc_Date_Edit")
            Dim d_doc As String = Convert.ToDateTime(TextBox_Doc_Date_Edit.Text).ToString("yyyy-MM-dd")
            MySqlComand.Parameters("@Date").Value = d_doc
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@Organization", SqlDbType.VarChar, 100))
            Dim TextBox_Organization_Edit As TextBox = GridView_Doc.Rows(GridView_Doc.EditIndex).FindControl("TextBox_Organization_Edit")
            MySqlComand.Parameters("@Organization").Value = TextBox_Organization_Edit.Text
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@Code", SqlDbType.VarChar, 50))
            Dim TextBox_Code_Edit As TextBox = GridView_Doc.Rows(GridView_Doc.EditIndex).FindControl("TextBox_Code_Edit")
            MySqlComand.Parameters("@Code").Value = TextBox_Code_Edit.Text
            '---------------------------------------------------------------------------------------
        End If
        '---------------------------------Добавление документа--------------------------------------
        If type = 3 Then
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@type", SqlDbType.Int))
            MySqlComand.Parameters("@type").Value = 3
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@User_GUID", SqlDbType.UniqueIdentifier))
            MySqlComand.Parameters("@User_GUID").Value = GUID_user
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@OPER_GUID", SqlDbType.VarChar, 50))
            MySqlComand.Parameters("@OPER_GUID").Value = s1
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@APP_GUID", SqlDbType.VarChar, 50))
            MySqlComand.Parameters("@APP_GUID").Value = ConfigurationManager.AppSettings.Item("app_guid").ToString
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@HOST", SqlDbType.VarChar, 50))
            MySqlComand.Parameters("@HOST").Value = HttpContext.Current.Request.UserHostAddress
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@Type_doc", SqlDbType.Int))
            'Dim TextBox_Series_Edit As TextBox = GridView_Doc.Rows(GridView_Doc.EditIndex).FindControl("TextBox_Series_Edit")
            MySqlComand.Parameters("@Type_doc").Value = DropDownList_User_Doc_Add_Type.SelectedValue
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@Series", SqlDbType.VarChar, 50))
            'Dim TextBox_Series_Edit As TextBox = GridView_Doc.Rows(GridView_Doc.EditIndex).FindControl("TextBox_Series_Edit")
            MySqlComand.Parameters("@Series").Value = TextBox_User_Doc_Add_Series.Text
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@Number", SqlDbType.VarChar, 50))
            'Dim TextBox_Number_Edit As TextBox = GridView_Doc.Rows(GridView_User_info.EditIndex).FindControl("TextBox_Number_Edit")
            MySqlComand.Parameters("@Number").Value = TextBox_User_Doc_Add_Number.Text
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@Date", SqlDbType.Date))
            'Dim TextBox_Doc_Date_Edit As TextBox = GridView_Doc.Rows(GridView_Doc.EditIndex).FindControl("TextBox_Doc_Date_Edit")
            Dim d_doc_add As String = Convert.ToDateTime(TextBox_User_Doc_Add_Date.Text).ToString("yyyy-MM-dd")
            MySqlComand.Parameters("@Date").Value = d_doc_add
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@Organization", SqlDbType.VarChar, 100))
            'Dim TextBox_Organization_Edit As TextBox = GridView_Doc.Rows(GridView_Doc.EditIndex).FindControl("TextBox_Organization_Edit")
            MySqlComand.Parameters("@Organization").Value = TextBox_User_Doc_Add_Organization.Text
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@Code", SqlDbType.VarChar, 50))
            MySqlComand.Parameters("@Code").Value = TextBox_User_Doc_Add_Code.Text
            '---------------------------------------------------------------------------------------
        End If
        '-------------------------------Удаление документа--------------------------------------------
        If type = 4 Then
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@type", SqlDbType.Int))
            MySqlComand.Parameters("@type").Value = 4
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@ID_User_doc", SqlDbType.Int))
            MySqlComand.Parameters("@ID_User_doc").Value = GridView_Doc.DataKeys.Item(index_del_doc).Value
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@OPER_GUID", SqlDbType.VarChar, 50))
            MySqlComand.Parameters("@OPER_GUID").Value = s1
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@APP_GUID", SqlDbType.VarChar, 50))
            MySqlComand.Parameters("@APP_GUID").Value = ConfigurationManager.AppSettings.Item("app_guid").ToString
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@HOST", SqlDbType.VarChar, 50))
            MySqlComand.Parameters("@HOST").Value = HttpContext.Current.Request.UserHostAddress
            '---------------------------------------------------------------------------------------
        End If
        MyConnection.Open()
        MySqlComand.ExecuteNonQuery()
        MyConnection.Close()
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Клик по картинке "Редактировать" в гриде "Документы" -----------------------------------------------------------------------------------------------------------------
    Protected Sub GridView_Doc_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles GridView_Doc.RowEditing
        GridView_Doc.EditIndex = e.NewEditIndex
        'Dim ID_user_doc As Int32 = Convert.ToInt64(GridView_Doc.DataKeys(GridView_Doc.EditIndex).Value)
        OCC_FAMILIA_SEARCH_EDIT_USER_DOC(GUID_user)
        'GridView_Doc.DataBind()
        UpdatePanel_User_info.Update()
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Клик по картинке "Сохранить" в гриде "Документы" ---------------------------------------------------------------------------------------------------------------------
    Protected Sub GridView_Doc_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles GridView_Doc.RowUpdating
        Try
            Label_User_Update_Error.Text = ""
            USER_DOC_UPDATE(GridView_Doc.DataKeys.Item(e.RowIndex).Value, 1)
            GridView_Doc.EditIndex = -1
            OCC_FAMILIA_SEARCH_EDIT_USER_DOC(GUID_user)
            UpdatePanel_User_info.Update()
        Catch ex As Exception
            Label_User_Update_Error.Text = "Ошибка!!! " + ex.Message
            Label_User_Update_Error.ForeColor = Drawing.Color.Red
            UpdatePanel_User_info.Update()

            'MsgBox(ex.Message)
        End Try
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Клик по картинке "Отменить" в гриде "Документы"-----------------------------------------------------------------------------------------------------------------------
    Protected Sub GridView_Doc_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles GridView_Doc.RowCancelingEdit
        Label_User_Update_Error.Text = ""
        GridView_Doc.EditIndex = -1
        OCC_FAMILIA_SEARCH_EDIT_USER_DOC(GUID_user)
        'GridView_Doc.DataBind()
        UpdatePanel_User_info.Update()
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Нажатие на кнопку "Добавить" (вызов модального окна "Добавление документа человека")----------------------------------------------------------------------------------
    Protected Sub Button_Doc_Add_Click(sender As Object, e As EventArgs)
        Dim MyConnection As SqlConnection
        Dim MyDataAdapter As SqlDataAdapter
        Dim con1 As String
        Dim s1 As String = FormsAuthentication.Decrypt(Request.Cookies("AppBillingCookie").Value).UserData
        con1 = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
        MyConnection = New SqlConnection(con1)
        MyDataAdapter = New SqlDataAdapter("APP_USER_COMBO", MyConnection)
        MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@OPER_GUID", SqlDbType.VarChar, 50))
        MyDataAdapter.SelectCommand.Parameters("@OPER_GUID").Value = s1
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@APP_GUID", SqlDbType.VarChar, 50))
        MyDataAdapter.SelectCommand.Parameters("@APP_GUID").Value = ConfigurationManager.AppSettings.Item("app_guid").ToString
        '---------------------------------------------------------------------------------------
        Dim DS As DataSet
        DS = New DataSet()
        MyDataAdapter.Fill(DS)
        Dim dv4 As DataView = New DataView(DS.Tables(4))

        DropDownList_User_Doc_Add_Type.DataSource = dv4
        DropDownList_User_Doc_Add_Type.DataTextField = "Type"
        DropDownList_User_Doc_Add_Type.DataValueField = "ID_doc_type"
        DropDownList_User_Doc_Add_Type.DataBind()
        DropDownList_User_Doc_Add_Type.SelectedValue = 0
        UpdatePanel_User_Doc_Add.Update()
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal_add_user_doc", "$('#myModal_add_user_doc').modal({backdrop: 'static',keyboard: false });", True)
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Клик по картинке "Удалить" грида "Документы" (вызов модального окна "Удаление документа человека")--------------------------------------------------------------------
    Protected Sub GridView_Doc_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles GridView_Doc.RowDeleting
        index_del_doc = e.RowIndex
        'MsgBox(GridView_Doc.DataKeys.Item(index_del_doc).Value)
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal_delete_user_doc", "$('#myModal_delete_user_doc').modal({backdrop: 'static',keyboard: false });", True)
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Нажатие на кнопку "Удалить" модального окна "Удаление документа человека"---------------------------------------------------------------------------------------------
    Protected Sub Button_Doc_Del_Click(sender As Object, e As EventArgs) ' Handles Button_myModal_Arhiv_LS.Click
        Try
            USER_DOC_UPDATE(GridView_Doc.DataKeys.Item(index_del_doc).Value, 4)

            OCC_FAMILIA_SEARCH_EDIT_USER_DOC(GUID_user)
            UpdatePanel_User_info.Update()
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal_delete_user_doc", "$('#myModal_delete_user_doc').modal('hide');", True)
        Catch ex As Exception
            Label_User_Doc_Delete_Error1.Text = "Ошибка"
            Label_User_Doc_Delete_Error2.ForeColor = Drawing.Color.Blue
            Label_User_Doc_Delete_Error2.Text = ex.Message
            UpdatePanel_User_doc_delete.Update()
        End Try
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Нажатие на кнопку "Добавить" модального окна "Добавление документа человека"------------------------------------------------------------------------------------------
    Protected Sub Button_User_Doc_Add_Click(sender As Object, e As EventArgs)
        Try
            USER_DOC_UPDATE(Nothing, 3)
            OCC_FAMILIA_SEARCH_EDIT_USER_DOC(GUID_user)
            UpdatePanel_User_info.Update()
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal_add_user_doc", "$('#myModal_add_user_doc').modal('hide');", True)
        Catch ex As Exception
            Label_Add_User_Doc_Error1.Text = "Ошибка"
            Label_Add_User_Doc_Error2.ForeColor = Drawing.Color.Blue
            Label_Add_User_Doc_Error2.Text = ex.Message
            UpdatePanel_User_Doc_Add.Update()
        End Try


    End Sub
    '                                                                                                                                                                                              |
    '***********************************************************************************************************************************************************************************************



    '***********************************************************************************************************************************************************************************************
    '|||||||||||||||||||||||| ВКЛАДКА "ПРИБОРЫ УЧЕТА" ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    '***********************************************************************************************************************************************************************************************
    '                                                                                                                                                                                              |
    '-------------------------Функция (Заполнение грида "Счетчики")---------------------------------------------------------------------------------------------------------------------------------
    Protected Sub COUNTERS(LS As Int64)
        Label_Counters_Header.Text = "Л/С №" & LS.ToString() & " адрес " & ADDR
        Dim MyConnection As SqlConnection
        Dim MyDataAdapter As SqlDataAdapter
        Dim con1 As String
        Dim s1 As String = FormsAuthentication.Decrypt(Request.Cookies("AppBillingCookie").Value).UserData
        con1 = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
        MyConnection = New SqlConnection(con1)
        MyDataAdapter = New SqlDataAdapter("APP_COUNTERS", MyConnection)
        MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@LS", SqlDbType.BigInt))
        MyDataAdapter.SelectCommand.Parameters("@LS").Value = LS
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@OPER_GUID", SqlDbType.VarChar, 50))
        MyDataAdapter.SelectCommand.Parameters("@OPER_GUID").Value = s1
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@APP_GUID", SqlDbType.VarChar, 50))
        MyDataAdapter.SelectCommand.Parameters("@APP_GUID").Value = ConfigurationManager.AppSettings.Item("app_guid").ToString
        '---------------------------------------------------------------------------------------
        Dim DS As DataSet
        DS = New DataSet()
        MyDataAdapter.Fill(DS)
        GridView_Counters.DataSource = DS.Tables(0).DefaultView
        GridView_Counters.AutoGenerateColumns = False
        GridView_Counters.Visible = True
        GridView_Counters.DataBind()
        Dim dv1 As DataView = New DataView(DS.Tables(1))
        For i As Integer = 0 To GridView_Counters.Rows.Count - 1
            Dim DropDownList_Counters_Service_Edit As DropDownList = GridView_Counters.Rows(i).FindControl("DropDownList_Counters_Service_Edit")
            If IsNothing(DropDownList_Counters_Service_Edit) Then
            Else
                DropDownList_Counters_Service_Edit.DataSource = dv1
                DropDownList_Counters_Service_Edit.DataTextField = "ID_Service"
                DropDownList_Counters_Service_Edit.DataValueField = "ID_Service"
                DropDownList_Counters_Service_Edit.DataBind()
                DropDownList_Counters_Service_Edit.SelectedValue = DS.Tables(0).Rows.Item(i).Item(2)
            End If
        Next
        UpdatePanel_Counters.Update()
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Функция "РЕДАКТИРОВАНИЕ, АРХИВАЦИЯ СЧЕТЧИКОВ"-------------------------------------------------------------------------------------------------------------------------
    Protected Sub COUNTERS_EDIT(COUNTER_ID As Integer, type As Integer)
        Try
            Dim MyConnection As SqlConnection
            Dim MySqlComand As SqlCommand
            Dim con1 As String
            Dim s1 As String = FormsAuthentication.Decrypt(Request.Cookies("AppBillingCookie").Value).UserData
            con1 = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
            MyConnection = New SqlConnection(con1)
            MySqlComand = New SqlCommand("APP_COUNTERS_EDIT", MyConnection)
            MySqlComand.CommandType = CommandType.StoredProcedure
            '---------------------------------Редактирование счетчика-----------------------------------
            If type = 1 Then
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@type", SqlDbType.Int))
                MySqlComand.Parameters("@type").Value = 1
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@COUNTER_ID", SqlDbType.Int))
                MySqlComand.Parameters("@COUNTER_ID").Value = COUNTER_ID
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@LS", SqlDbType.BigInt))
                MySqlComand.Parameters("@LS").Value = LS
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@OPER_GUID", SqlDbType.VarChar, 50))
                MySqlComand.Parameters("@OPER_GUID").Value = s1
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@APP_GUID", SqlDbType.VarChar, 50))
                MySqlComand.Parameters("@APP_GUID").Value = ConfigurationManager.AppSettings.Item("app_guid").ToString
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@HOST", SqlDbType.VarChar, 50))
                MySqlComand.Parameters("@HOST").Value = HttpContext.Current.Request.UserHostAddress
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@SERVICE_ID", SqlDbType.Char, 4))
                Dim TextBox_Counters_Service_Edit As TextBox = GridView_Counters.Rows(GridView_Counters.EditIndex).FindControl("TextBox_Counters_Service_Edit")
                MySqlComand.Parameters("@SERVICE_ID").Value = TextBox_Counters_Service_Edit.Text
                '-----------------------------Серийный номер--------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@SERIAL_NUM", SqlDbType.VarChar, 150))
                Dim TextBox_Counters_Serial_Num_Edit As TextBox = GridView_Counters.Rows(GridView_Counters.EditIndex).FindControl("TextBox_Counters_Serial_Num_Edit")
                MySqlComand.Parameters("@SERIAL_NUM").Value = TextBox_Counters_Serial_Num_Edit.Text
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@COUNTER_MODEL_ID", SqlDbType.Int))
                MySqlComand.Parameters("@COUNTER_MODEL_ID").Value = 1 'из грида
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@COUNTER_TYPE_ID", SqlDbType.Int))
                MySqlComand.Parameters("@COUNTER_TYPE_ID").Value = 1 'из грида
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@MAX_VALUE", SqlDbType.Decimal, 18, 4))
                Dim TextBox_Counters_Max_Value_Edit As TextBox = GridView_Counters.Rows(GridView_Counters.EditIndex).FindControl("TextBox_Counters_Max_Value_Edit")
                MySqlComand.Parameters("@MAX_VALUE").Value = TextBox_Counters_Max_Value_Edit.Text
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@LAST_VALUE", SqlDbType.Decimal, 18, 4))
                Dim TextBox_Counters_Last_Value_Edit As TextBox = GridView_Counters.Rows(GridView_Counters.EditIndex).FindControl("TextBox_Counters_Last_Value_Edit")
                MySqlComand.Parameters("@LAST_VALUE").Value = TextBox_Counters_Last_Value_Edit.Text
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@CUR_VALUE", SqlDbType.Decimal, 18, 4))
                Dim TextBox_Counters_Cur_Value_Edit As TextBox = GridView_Counters.Rows(GridView_Counters.EditIndex).FindControl("TextBox_Counters_Cur_Value_Edit")
                MySqlComand.Parameters("@CUR_VALUE").Value = TextBox_Counters_Cur_Value_Edit.Text
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@CUR_UNITS", SqlDbType.Decimal, 18, 4))
                Dim TextBox_Counters_Cur_Units_Edit As TextBox = GridView_Counters.Rows(GridView_Counters.EditIndex).FindControl("TextBox_Counters_Cur_Units_Edit")
                MySqlComand.Parameters("@CUR_UNITS").Value = TextBox_Counters_Cur_Units_Edit.Text
                '----------------------------Дата последних показаний-----------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@LAST_DATE", SqlDbType.Date))
                Dim TextBox_Counters_Last_Date_Edit As TextBox = GridView_Counters.Rows(GridView_Counters.EditIndex).FindControl("TextBox_Counters_Last_Date_Edit")
                Dim d_Last_date As Date
                If Date.TryParse(TextBox_Counters_Last_Date_Edit.Text, d_Last_date) Then
                    d_Last_date = Convert.ToDateTime(TextBox_Counters_Last_Date_Edit.Text).ToString("yyyy-MM-dd")
                    MySqlComand.Parameters("@LAST_DATE").Value = d_Last_date
                End If
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@COEFF", SqlDbType.Decimal, 18, 2))
                Dim TextBox_Counters_Coeff_Edit As TextBox = GridView_Counters.Rows(GridView_Counters.EditIndex).FindControl("TextBox_Counters_Coeff_Edit")
                MySqlComand.Parameters("@COEFF").Value = TextBox_Counters_Coeff_Edit.Text
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@NORM", SqlDbType.Decimal, 18, 4))
                Dim TextBox_Counters_Norm_Edit As TextBox = GridView_Counters.Rows(GridView_Counters.EditIndex).FindControl("TextBox_Counters_Norm_Edit")
                MySqlComand.Parameters("@NORM").Value = TextBox_Counters_Norm_Edit.Text
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@CALC_PEOPLE", SqlDbType.Int))
                Dim TextBox_Calc_People_Edit As TextBox = GridView_Counters.Rows(GridView_Counters.EditIndex).FindControl("TextBox_Calc_People_Edit")
                MySqlComand.Parameters("@CALC_PEOPLE").Value = TextBox_Calc_People_Edit.Text
                '---------------------------Дата заведения счетчика-------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@DATE_CNTR", SqlDbType.Date))
                Dim TextBox_Date_Cntr_Edit As TextBox = GridView_Counters.Rows(GridView_Counters.EditIndex).FindControl("TextBox_Date_Cntr_Edit")
                Dim d_DATE_CNTR As String = Convert.ToDateTime(TextBox_Date_Cntr_Edit.Text).ToString("yyyy-MM-dd")
                MySqlComand.Parameters("@DATE_CNTR").Value = d_DATE_CNTR
                '---------------------------Дата установки счетчика-------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@Installation_date", SqlDbType.Date))
                Dim TextBox_Installation_Date_Edit As TextBox = GridView_Counters.Rows(GridView_Counters.EditIndex).FindControl("TextBox_Installation_Date_Edit")
                Dim d_Installation_date As String = Convert.ToDateTime(TextBox_Installation_Date_Edit.Text).ToString("yyyy-MM-dd")
                MySqlComand.Parameters("@Installation_date").Value = d_Installation_date
                '---------------------------Дата ввода в эксплуатацию-----------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@Date_of_commissioning", SqlDbType.Date))
                Dim TextBox_Date_Of_Commissioning_Edit As TextBox = GridView_Counters.Rows(GridView_Counters.EditIndex).FindControl("TextBox_Date_Of_Commissioning_Edit")
                Dim d_Date_of_commissioning As String = Convert.ToDateTime(TextBox_Date_Of_Commissioning_Edit.Text).ToString("yyyy-MM-dd")
                MySqlComand.Parameters("@Date_of_commissioning").Value = d_Date_of_commissioning
                '---------------------------Дата пломбировки--------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@Date_plant_mark", SqlDbType.Date))
                Dim TextBox_Date_Plant_Mark_Edit As TextBox = GridView_Counters.Rows(GridView_Counters.EditIndex).FindControl("TextBox_Date_Plant_Mark_Edit")
                Dim d_Date_plant_mark As Date
                If Date.TryParse(TextBox_Date_Plant_Mark_Edit.Text, d_Date_plant_mark) Then
                    d_Date_plant_mark = Convert.ToDateTime(TextBox_Date_Plant_Mark_Edit.Text).ToString("yyyy-MM-dd")
                    MySqlComand.Parameters("@Date_plant_mark").Value = d_Date_plant_mark
                End If
                '---------------------------Дата следующей поверки--------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@Date_of_last_verification", SqlDbType.Date))
                Dim TextBox_Date_Of_Last_Verification_Edit As TextBox = GridView_Counters.Rows(GridView_Counters.EditIndex).FindControl("TextBox_Date_Of_Last_Verification_Edit")
                Dim d_Date_of_last_verification As Date
                If Date.TryParse(TextBox_Date_Of_Last_Verification_Edit.Text, d_Date_of_last_verification) Then
                    d_Date_of_last_verification = Convert.ToDateTime(TextBox_Date_Of_Last_Verification_Edit.Text).ToString("yyyy-MM-dd")
                    MySqlComand.Parameters("@Date_of_last_verification").Value = d_Date_of_last_verification
                End If
                '---------------------------------------------------------------------------------------
            End If
            '-------------------------------УДАЛЕНИЕ СЧЕТЧИКА-------------------------------------------
            If type = 4 Then
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@type", SqlDbType.Int))
                MySqlComand.Parameters("@type").Value = 4
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@COUNTER_ID", SqlDbType.Int))
                MySqlComand.Parameters("@COUNTER_ID").Value = COUNTER_ID
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@LS", SqlDbType.BigInt))
                MySqlComand.Parameters("@LS").Value = LS
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@OPER_GUID", SqlDbType.VarChar, 50))
                MySqlComand.Parameters("@OPER_GUID").Value = s1
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@APP_GUID", SqlDbType.VarChar, 50))
                MySqlComand.Parameters("@APP_GUID").Value = ConfigurationManager.AppSettings.Item("app_guid").ToString
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@HOST", SqlDbType.VarChar, 50))
                MySqlComand.Parameters("@HOST").Value = HttpContext.Current.Request.UserHostAddress
                '---------------------------------------------------------------------------------------
            End If
            MyConnection.Open()
            MySqlComand.ExecuteNonQuery()
            MyConnection.Close()
        Catch ex As Exception
            Label_Counters_Error.Text = ex.Message
        End Try
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Нажатие на кнопку "Добавить прибор" (вызов модального окна "Добавить прибор")-----------------------------------------------------------------------------------------
    Protected Sub Button_Counter_Add_Click(sender As Object, e As EventArgs) Handles Button_Counter_Add.Click
        TextBox_Counter_Add_Current.Text = ""
        TextBox_Counter_Add_Koeff.Text = ""
        TextBox_Counter_Add_Last.Text = ""
        TextBox_Counter_Add_Max.Text = ""
        TextBox_Counter_Add_Serial_Number.Text = ""
        TextBox_Counter_Add_Service.Text = ""
        Label_Counters_Error.Text = ""
        Label_Counter_Add_Error1.Text = ""
        Label_Counter_Add_Error2.Text = ""
        TextBox_Counter_Add_Date_Install.Text = Format(Now.Date, "yyyy-MM-dd")
        TextBox_Counter_Add_Date_Comission.Text = Format(Now.Date, "yyyy-MM-dd")
        TextBox_Counter_Add_Date_Plant_Mark.Text = Format(Now.Date, "yyyy-MM-dd")
        Dim MyConnection As SqlConnection
        Dim MyDataAdapter As SqlDataAdapter
        Dim con1 As String
        Dim s1 As String = FormsAuthentication.Decrypt(Request.Cookies("AppBillingCookie").Value).UserData
        con1 = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
        MyConnection = New SqlConnection(con1)

        MyDataAdapter = New SqlDataAdapter("APP_COUNTER_COMBO", MyConnection)
        MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@OPER_GUID", SqlDbType.VarChar, 50))
        MyDataAdapter.SelectCommand.Parameters("@OPER_GUID").Value = s1
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@APP_GUID", SqlDbType.VarChar, 50))
        MyDataAdapter.SelectCommand.Parameters("@APP_GUID").Value = ConfigurationManager.AppSettings.Item("app_guid").ToString
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@LS", SqlDbType.BigInt))
        MyDataAdapter.SelectCommand.Parameters("@LS").Value = LS
        '---------------------------------------------------------------------------------------
        Dim DS As DataSet
        DS = New DataSet()
        MyDataAdapter.Fill(DS)

        Dim dv0 As DataView = New DataView(DS.Tables(0))
        Dim dv1 As DataView = New DataView(DS.Tables(1))
        Dim dv2 As DataView = New DataView(DS.Tables(2))
        '--------------Если для данного Л/С нет приборов----------------------------------------
        If (DS.Tables(1).Rows.Count > 0) Then
            DropDownList_Counter_Add_Model.DataSource = dv1
            DropDownList_Counter_Add_Model.DataTextField = "MODEL_NAME"
            DropDownList_Counter_Add_Model.DataValueField = "MODEL_ID"
            DropDownList_Counter_Add_Model.DataBind()

            MyDataAdapter = New SqlDataAdapter("APP_COUNTER_MODEL", MyConnection)
            MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            '---------------------------------------------------------------------------------------
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@COUNTER_MODEL_ID", SqlDbType.VarChar, 50))
            MyDataAdapter.SelectCommand.Parameters("@COUNTER_MODEL_ID").Value = DropDownList_Counter_Add_Model.SelectedValue
            '---------------------------------------------------------------------------------------
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@OPER_GUID", SqlDbType.VarChar, 50))
            MyDataAdapter.SelectCommand.Parameters("@OPER_GUID").Value = s1
            '---------------------------------------------------------------------------------------
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@APP_GUID", SqlDbType.VarChar, 50))
            MyDataAdapter.SelectCommand.Parameters("@APP_GUID").Value = ConfigurationManager.AppSettings.Item("app_guid").ToString
            '---------------------------------------------------------------------------------------
            DS = New DataSet()
            MyDataAdapter.Fill(DS)
            TextBox_Counter_Add_Service.Text = DS.Tables(0).Rows.Item(0).Item(0)
            For i As Integer = 1 To DS.Tables(0).Rows.Item(0).Item(2)
                TextBox_Counter_Add_Max.Text = TextBox_Counter_Add_Max.Text & "9"
            Next
            TextBox_Counter_Add_Max.Text = TextBox_Counter_Add_Max.Text & "."
            For i As Integer = 1 To DS.Tables(0).Rows.Item(0).Item(3)
                TextBox_Counter_Add_Max.Text = TextBox_Counter_Add_Max.Text & "9"
            Next
            TextBox_Counter_Add_Koeff.Text = DS.Tables(0).Rows.Item(0).Item(4)
            Dim D As Date = Now
            D = D.AddMonths(DS.Tables(0).Rows.Item(0).Item(5))
            TextBox_Counter_Add_Date_Of_Last_Verification.Text = Format(D, "yyyy-MM-dd")
            dv0 = New DataView(DS.Tables(0))
            DropDownList_Counter_Add_Type.DataSource = dv0
            DropDownList_Counter_Add_Type.DataTextField = "CNTR_Type"
            DropDownList_Counter_Add_Type.DataValueField = "CNTR_RES_ID"
            DropDownList_Counter_Add_Type.DataBind()
            If DropDownList_Counter_Add_Type.Items.Count > 0 Then
                DropDownList_Counter_Add_Type.SelectedIndex = 0
            End If
            TextBox_Counter_Add_Current.Visible = True
            TextBox_Counter_Add_Koeff.Visible = True
            TextBox_Counter_Add_Last.Visible = True
            TextBox_Counter_Add_Max.Visible = True
            TextBox_Counter_Add_Serial_Number.Visible = True
            TextBox_Counter_Add_Service.Visible = True
            TextBox_Counter_Add_Date_Install.Visible = True
            TextBox_Counter_Add_Date_Comission.Visible = True
            TextBox_Counter_Add_Date_Plant_Mark.Visible = True
            Button_Counter_Add_Complete.Visible = True
            Button_Counter_Add_Complete.Visible = True
            DropDownList_Counter_Add_Model.Visible = True
            DropDownList_Counter_Add_Type.Visible = True
            TextBox_Counter_Add_Date_Of_Last_Verification.Visible = True
            UpdatePanel_Counter_Add.Update()
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal_Counter_Add", "$('#myModal_Counter_Add').modal({backdrop: 'static',keyboard: false });", True)
        Else
            TextBox_Counter_Add_Current.Visible = False
            TextBox_Counter_Add_Koeff.Visible = False
            TextBox_Counter_Add_Last.Visible = False
            TextBox_Counter_Add_Max.Visible = False
            TextBox_Counter_Add_Serial_Number.Visible = False
            TextBox_Counter_Add_Service.Visible = False
            TextBox_Counter_Add_Date_Install.Visible = False
            TextBox_Counter_Add_Date_Comission.Visible = False
            TextBox_Counter_Add_Date_Plant_Mark.Visible = False
            Button_Counter_Add_Complete.Visible = False
            DropDownList_Counter_Add_Model.Visible = False
            DropDownList_Counter_Add_Type.Visible = False
            TextBox_Counter_Add_Date_Of_Last_Verification.Visible = False
            Label_Counter_Add_Error1.Text = "Обратите внимание!"
            Label_Counter_Add_Error2.Text = "Для данного Л/С нет приборов."
            UpdatePanel_Counter_Add.Update()
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal_Counter_Add", "$('#myModal_Counter_Add').modal({backdrop: 'static',keyboard: false });", True)
        End If

    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Выбор модели прибора из выпадающего списка----------------------------------------------------------------------------------------------------------------------------
    Protected Sub DropDownList_Counter_Add_Model_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList_Counter_Add_Model.SelectedIndexChanged
        TextBox_Counter_Add_Max.Text = ""
        TextBox_Counter_Add_Service.Text = ""
        TextBox_Counter_Add_Koeff.Text = ""
        Dim MyConnection As SqlConnection
        Dim MyDataAdapter As SqlDataAdapter
        Dim con1 As String
        Dim s1 As String = FormsAuthentication.Decrypt(Request.Cookies("AppBillingCookie").Value).UserData
        con1 = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
        MyConnection = New SqlConnection(con1)
        MyDataAdapter = New SqlDataAdapter("APP_COUNTER_MODEL", MyConnection)
        MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@COUNTER_MODEL_ID", SqlDbType.VarChar, 50))
        MyDataAdapter.SelectCommand.Parameters("@COUNTER_MODEL_ID").Value = DropDownList_Counter_Add_Model.SelectedValue
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@OPER_GUID", SqlDbType.VarChar, 50))
        MyDataAdapter.SelectCommand.Parameters("@OPER_GUID").Value = s1
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@APP_GUID", SqlDbType.VarChar, 50))
        MyDataAdapter.SelectCommand.Parameters("@APP_GUID").Value = ConfigurationManager.AppSettings.Item("app_guid").ToString
        '---------------------------------------------------------------------------------------
        Dim DS As DataSet
        DS = New DataSet()
        MyDataAdapter.Fill(DS)
        'TextBox_Counter_Add_Type.Text = DS.Tables(0).Rows.Item(0).Item(7)
        TextBox_Counter_Add_Service.Text = DS.Tables(0).Rows.Item(0).Item(0)
        For i As Integer = 1 To DS.Tables(0).Rows.Item(0).Item(2)
            TextBox_Counter_Add_Max.Text = TextBox_Counter_Add_Max.Text & "9"
        Next
        TextBox_Counter_Add_Max.Text = TextBox_Counter_Add_Max.Text & "."
        For i As Integer = 1 To DS.Tables(0).Rows.Item(0).Item(3)
            TextBox_Counter_Add_Max.Text = TextBox_Counter_Add_Max.Text & "9"
        Next
        TextBox_Counter_Add_Koeff.Text = DS.Tables(0).Rows.Item(0).Item(4)
        Dim D As Date = Now
        D = D.AddMonths(DS.Tables(0).Rows.Item(0).Item(5))
        TextBox_Counter_Add_Date_Of_Last_Verification.Text = Format(D, "yyyy-MM-dd")
        Dim dv0 As DataView = New DataView(DS.Tables(0))
        DropDownList_Counter_Add_Type.DataSource = dv0
        DropDownList_Counter_Add_Type.DataTextField = "CNTR_Type"
        DropDownList_Counter_Add_Type.DataValueField = "CNTR_RES_ID"
        DropDownList_Counter_Add_Type.DataBind()
        If DropDownList_Counter_Add_Type.Items.Count > 0 Then
            DropDownList_Counter_Add_Type.SelectedIndex = 0
        End If
        UpdatePanel_Counter_Add.Update()
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Нажатие на кнопку "Добавить" модального окна "Добавление прибора"-----------------------------------------------------------------------------------------------------
    Protected Sub Button_Counter_Add_Complete_Click(sender As Object, e As EventArgs)
        Try
            'COUNTERS_EDIT(Nothing, 3)
            Dim MyConnection As SqlConnection
            Dim MySqlComand As SqlCommand
            Dim con1 As String
            Dim s1 As String = FormsAuthentication.Decrypt(Request.Cookies("AppBillingCookie").Value).UserData
            con1 = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
            MyConnection = New SqlConnection(con1)
            MySqlComand = New SqlCommand("APP_COUNTERS_EDIT", MyConnection)
            MySqlComand.CommandType = CommandType.StoredProcedure
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@type", SqlDbType.Int))
            MySqlComand.Parameters("@type").Value = 3
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@OPER_GUID", SqlDbType.VarChar, 50))
            MySqlComand.Parameters("@OPER_GUID").Value = s1
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@APP_GUID", SqlDbType.VarChar, 50))
            MySqlComand.Parameters("@APP_GUID").Value = ConfigurationManager.AppSettings.Item("app_guid").ToString
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@HOST", SqlDbType.VarChar, 50))
            MySqlComand.Parameters("@HOST").Value = HttpContext.Current.Request.UserHostAddress
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@LS", SqlDbType.BigInt))
            MySqlComand.Parameters("@LS").Value = LS
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@SERVICE_ID", SqlDbType.Char, 4))
            MySqlComand.Parameters("@SERVICE_ID").Value = TextBox_Counter_Add_Service.Text
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@SERIAL_NUM", SqlDbType.VarChar, 150))
            MySqlComand.Parameters("@SERIAL_NUM").Value = TextBox_Counter_Add_Serial_Number.Text
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@COUNTER_MODEL_ID", SqlDbType.Int))
            MySqlComand.Parameters("@COUNTER_MODEL_ID").Value = DropDownList_Counter_Add_Model.SelectedValue
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@COUNTER_TYPE_ID", SqlDbType.Int))
            MySqlComand.Parameters("@COUNTER_TYPE_ID").Value = DropDownList_Counter_Add_Type.SelectedValue
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@MAX_VALUE", SqlDbType.Decimal, 18, 4))
            MySqlComand.Parameters("@MAX_VALUE").Value = Convert.ToDecimal(TextBox_Counter_Add_Max.Text)
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@LAST_VALUE", SqlDbType.Decimal, 18, 4))
            If TextBox_Counter_Add_Last.Text = "" Then
                MySqlComand.Parameters("@LAST_VALUE").Value = Nothing
            Else
                MySqlComand.Parameters("@LAST_VALUE").Value = Convert.ToDecimal(TextBox_Counter_Add_Last.Text)
            End If
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@CUR_VALUE", SqlDbType.Decimal, 18, 4))
            If TextBox_Counter_Add_Current.Text = "" Then
                MySqlComand.Parameters("@CUR_VALUE").Value = Nothing
            Else
                MySqlComand.Parameters("@CUR_VALUE").Value = Convert.ToDecimal(TextBox_Counter_Add_Current.Text)
            End If
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@CUR_UNITS", SqlDbType.Decimal, 18, 4))
            MySqlComand.Parameters("@CUR_UNITS").Value = 1
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@LAST_DATE", SqlDbType.Date))
            MySqlComand.Parameters("@LAST_DATE").Value = Nothing 'не обязательно
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@COEFF", SqlDbType.Decimal, 18, 2))
            MySqlComand.Parameters("@COEFF").Value = Convert.ToDecimal(TextBox_Counter_Add_Koeff.Text)
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@NORM", SqlDbType.Decimal, 18, 4))
            MySqlComand.Parameters("@NORM").Value = 1
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@CALC_PEOPLE", SqlDbType.Int))
            MySqlComand.Parameters("@CALC_PEOPLE").Value = 1
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@DATE_CNTR", SqlDbType.Date))
            Dim d_DATE_CNTR As String = Convert.ToDateTime(TextBox_Counter_Add_Date_Install.Text).ToString("yyyy-MM-dd")
            MySqlComand.Parameters("@DATE_CNTR").Value = d_DATE_CNTR 'такая же как и дата установки счетчика
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@Installation_date", SqlDbType.Date))
            Dim d_Installation_date As String = Convert.ToDateTime(TextBox_Counter_Add_Date_Install.Text).ToString("yyyy-MM-dd")
            MySqlComand.Parameters("@Installation_date").Value = d_Installation_date
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@Date_of_commissioning", SqlDbType.Date))
            Dim d_Date_of_commissioning As String = Convert.ToDateTime(TextBox_Counter_Add_Date_Comission.Text).ToString("yyyy-MM-dd")
            MySqlComand.Parameters("@Date_of_commissioning").Value = d_Date_of_commissioning
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@Date_plant_mark", SqlDbType.Date))
            Dim d_Date_plant_mark As String = Convert.ToDateTime(TextBox_Counter_Add_Date_Plant_Mark.Text).ToString("yyyy-MM-dd")
            MySqlComand.Parameters("@Date_plant_mark").Value = d_Date_plant_mark
            '---------------------------------------------------------------------------------------
            MySqlComand.Parameters.Add(New SqlParameter("@Date_of_last_verification", SqlDbType.Date))
            Dim d_Date_of_last_verification As String = Convert.ToDateTime(TextBox_Counter_Add_Date_Of_Last_Verification.Text).ToString("yyyy-MM-dd")
            MySqlComand.Parameters("@Date_of_last_verification").Value = d_Date_of_last_verification
            '---------------------------------------------------------------------------------------
            MyConnection.Open()
            MySqlComand.ExecuteNonQuery()
            MyConnection.Close()
            COUNTERS(LS)
            UpdatePanel_Counters.Update()
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal_Counter_Add", "$('#myModal_Counter_Add').modal('hide');", True)
        Catch ex As Exception
            Label_Counter_Add_Error1.Text = "Ошибка"
            Label_Counter_Add_Error2.ForeColor = Drawing.Color.Blue
            Label_Counter_Add_Error2.Text = ex.Message
            UpdatePanel_Counter_Add.Update()
        End Try
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Клик по картинке "Редактировать" в гриде "Приборы учета" -------------------------------------------------------------------------------------------------------------
    Protected Sub GridView_Counters_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles GridView_Counters.RowEditing
        GridView_Counters.EditIndex = e.NewEditIndex
        COUNTERS(LS)
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Клик по картинке "Отменить" в гриде "Приборы учета"-------------------------------------------------------------------------------------------------------------------
    Protected Sub GridView_Counters_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles GridView_Counters.RowCancelingEdit
        Label_Counters_Error.Text = ""
        GridView_Counters.EditIndex = -1
        COUNTERS(LS)
        'GridView_Counters.DataBind()
        UpdatePanel_Counters.Update()
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Клик по картинке "Сохранить" в гриде "Приборы учета"------------------------------------------------------------------------------------------------------------------
    Protected Sub GridView_Counters_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles GridView_Counters.RowUpdating
        Try
            Label_Counters_Error.Text = ""
            COUNTERS_EDIT(GridView_Counters.DataKeys.Item(GridView_Counters.EditIndex).Value, 1)
            GridView_Counters.EditIndex = -1
            COUNTERS(LS)
            UpdatePanel_Counters.Update()
        Catch ex As Exception
            Label_Counters_Error.ForeColor = Drawing.Color.Red
            Label_Counters_Error.Text = "!!!Ошибка!!! " + ex.Message
            UpdatePanel_Counters.Update()
            e.Cancel = True
        End Try
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Клик по картинке "Удалить" грида "Приборы учета" (вызов модального окна "Удаление прибора учета")---------------------------------------------------------------------
    Protected Sub GridView_Counters_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles GridView_Counters.RowDeleting
        index_del_counter = e.RowIndex
        ''MsgBox(GridView_Counters.DataKeys.Item(index_del_counter).Value)
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal_Counter_delete", "$('#myModal_Counter_delete').modal({backdrop: 'static',keyboard: false });", True)
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Нажатие на кнопку "Удалить" модального окна "Удаление прибора"--------------------------------------------------------------------------------------------------------
    Protected Sub Button_Counter_Dell_Click(sender As Object, e As EventArgs) ' Handles Button_myModal_Arhiv_LS.Click
        'MsgBox(GridView_Counters.DataKeys.Item(index_del_counter).Value)
        Try
            COUNTERS_EDIT(GridView_Counters.DataKeys.Item(index_del_counter).Value, 4)
            COUNTERS(LS)
            UpdatePanel_Counter_delete.Update()
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal_Counter_delete", "$('#myModal_Counter_delete').modal('hide');", True)
        Catch ex As Exception
            Label_Counter_Delete_Error1.Text = "Ошибка"
            Label_Counter_Delete_Error2.ForeColor = Drawing.Color.Blue
            Label_Counter_Delete_Error2.Text = ex.Message
            UpdatePanel_Counter_delete.Update()
        End Try
    End Sub
    '                                                                                                                                                                                              |
    '***********************************************************************************************************************************************************************************************



    '***********************************************************************************************************************************************************************************************
    '|||||||||||||||||||||||| ВКЛАДКА "НАЧИСЛЕНИЯ" |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    '***********************************************************************************************************************************************************************************************
    '                                                                                                                                                                                              |
    '-------------------------Функция (Заполнение грида "Начисления")-------------------------------------------------------------------------------------------------------------------------------
    Protected Sub CALC(LS As Int64)
        Label_Calc_Header.Text = "Л/С №" & LS.ToString() & " адрес " & ADDR
        Dim MyConnection As SqlConnection
        Dim MyDataAdapter As SqlDataAdapter
        Dim con1 As String
        Dim s1 As String = FormsAuthentication.Decrypt(Request.Cookies("AppBillingCookie").Value).UserData
        con1 = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
        MyConnection = New SqlConnection(con1)
        MyDataAdapter = New SqlDataAdapter("APP_CALC", MyConnection)
        MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@LS", SqlDbType.BigInt))
        MyDataAdapter.SelectCommand.Parameters("@LS").Value = LS
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@OPER_GUID", SqlDbType.VarChar, 50))
        MyDataAdapter.SelectCommand.Parameters("@OPER_GUID").Value = s1
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@APP_GUID", SqlDbType.VarChar, 50))
        MyDataAdapter.SelectCommand.Parameters("@APP_GUID").Value = ConfigurationManager.AppSettings.Item("app_guid").ToString
        '---------------------------------------------------------------------------------------
        Dim DS As DataSet
        DS = New DataSet()
        MyDataAdapter.Fill(DS)
        GridView_Calc.DataSource = DS.Tables(0).DefaultView
        GridView_Calc.AutoGenerateColumns = False
        GridView_Calc.Visible = True
        GridView_Calc.DataBind()
        'Dim dv1 As DataView = New DataView(DS.Tables(1))
        'For i As Integer = 0 To GridView_Counters.Rows.Count - 1
        '    Dim DropDownList_Counters_Service_Edit As DropDownList = GridView_Counters.Rows(i).FindControl("DropDownList_Counters_Service_Edit")
        '    If IsNothing(DropDownList_Counters_Service_Edit) Then
        '    Else
        '        DropDownList_Counters_Service_Edit.DataSource = dv1
        '        DropDownList_Counters_Service_Edit.DataTextField = "ID_Service"
        '        DropDownList_Counters_Service_Edit.DataValueField = "ID_Service"
        '        DropDownList_Counters_Service_Edit.DataBind()
        '        DropDownList_Counters_Service_Edit.SelectedValue = DS.Tables(0).Rows.Item(i).Item(2)
        '    End If
        'Next
        UpdatePanel_Calc.Update()
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Функция "РЕДАКТИРОВАНИЕ, ДОБАВЛЕНИЕ, УДАЛЕНИЕ НАЧИСЛЕНИЙ"-------------------------------------------------------------------------------------------------------------
    Protected Sub CALC_EDIT(SERVICE_ID As String, type As Integer)
        Try
            Dim MyConnection As SqlConnection
            Dim MySqlComand As SqlCommand
            Dim con1 As String
            Dim s1 As String = FormsAuthentication.Decrypt(Request.Cookies("AppBillingCookie").Value).UserData
            con1 = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
            MyConnection = New SqlConnection(con1)
            MySqlComand = New SqlCommand("APP_CALC_EDIT", MyConnection)
            MySqlComand.CommandType = CommandType.StoredProcedure
            '---------------------------------Редактирование начисления-----------------------------------
            If type = 1 Then
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@type", SqlDbType.Int))
                MySqlComand.Parameters("@type").Value = 1
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@SERVICE_ID", SqlDbType.Char, 4))
                MySqlComand.Parameters("@SERVICE_ID").Value = SERVICE_ID
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@OCC_ID", SqlDbType.BigInt))
                MySqlComand.Parameters("@OCC_ID").Value = LS
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@OPER_GUID", SqlDbType.VarChar, 50))
                MySqlComand.Parameters("@OPER_GUID").Value = s1
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@APP_GUID", SqlDbType.VarChar, 50))
                MySqlComand.Parameters("@APP_GUID").Value = ConfigurationManager.AppSettings.Item("app_guid").ToString
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@HOST", SqlDbType.VarChar, 50))
                MySqlComand.Parameters("@HOST").Value = HttpContext.Current.Request.UserHostAddress
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@UNITS", SqlDbType.Decimal, 18, 6))
                Dim TextBox_Units_Edit As TextBox = GridView_Calc.Rows(GridView_Calc.EditIndex).FindControl("TextBox_Units_Edit")
                MySqlComand.Parameters("@UNITS").Value = Convert.ToDecimal(TextBox_Units_Edit.Text)
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@CNTR_UNITS", SqlDbType.Decimal, 18, 6))
                Dim TextBox_Cntr_Units_Edit As TextBox = GridView_Calc.Rows(GridView_Calc.EditIndex).FindControl("TextBox_Cntr_Units_Edit")
                MySqlComand.Parameters("@CNTR_UNITS").Value = Convert.ToDecimal(TextBox_Cntr_Units_Edit.Text)
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@NORM_UNITS", SqlDbType.Decimal, 18, 6))
                Dim TextBox_Norm_Units_Edit As TextBox = GridView_Calc.Rows(GridView_Calc.EditIndex).FindControl("TextBox_Norm_Units_Edit")
                MySqlComand.Parameters("@NORM_UNITS").Value = Convert.ToDecimal(TextBox_Norm_Units_Edit.Text)
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@NORM_COEFF", SqlDbType.Decimal, 18, 6))
                Dim TextBox_Norm_Coeff_Edit As TextBox = GridView_Calc.Rows(GridView_Calc.EditIndex).FindControl("TextBox_Norm_Coeff_Edit")
                MySqlComand.Parameters("@NORM_COEFF").Value = Convert.ToDecimal(TextBox_Norm_Coeff_Edit.Text)
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@USE_NORM_COEFF", SqlDbType.Int))
                Dim TextBox_Use_Norm_Coeff_Edit As TextBox = GridView_Calc.Rows(GridView_Calc.EditIndex).FindControl("TextBox_Use_Norm_Coeff_Edit")
                MySqlComand.Parameters("@USE_NORM_COEFF").Value = Convert.ToInt32(TextBox_Use_Norm_Coeff_Edit.Text)
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@PR_DAYS", SqlDbType.Int))
                Dim TextBox_Pr_Days_Edit As TextBox = GridView_Calc.Rows(GridView_Calc.EditIndex).FindControl("TextBox_Pr_Days_Edit")
                MySqlComand.Parameters("@PR_DAYS").Value = Convert.ToInt32(TextBox_Pr_Days_Edit.Text)
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@PR_MONTHS", SqlDbType.Int))
                Dim TextBox_Pr_Month_Edit As TextBox = GridView_Calc.Rows(GridView_Calc.EditIndex).FindControl("TextBox_Pr_Month_Edit")
                MySqlComand.Parameters("@PR_MONTHS").Value = Convert.ToInt32(TextBox_Pr_Month_Edit.Text)
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@LAST_PAID", SqlDbType.Date))
                Dim TextBox_Calc_Last_Paid_Edit As TextBox = GridView_Calc.Rows(GridView_Calc.EditIndex).FindControl("TextBox_Calc_Last_Paid_Edit")
                Dim d_Last_Paid As Date
                If Date.TryParse(TextBox_Calc_Last_Paid_Edit.Text, d_Last_Paid) Then
                    d_Last_Paid = Convert.ToDateTime(TextBox_Calc_Last_Paid_Edit.Text).ToString("yyyy-MM-dd")
                    MySqlComand.Parameters("@LAST_PAID").Value = d_Last_Paid
                End If
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@SALDO", SqlDbType.Money))
                Dim TextBox_Saldo_Edit As TextBox = GridView_Calc.Rows(GridView_Calc.EditIndex).FindControl("TextBox_Saldo_Edit")
                MySqlComand.Parameters("@SALDO").Value = Convert.ToDecimal(TextBox_Saldo_Edit.Text)
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@VALUE", SqlDbType.Money))
                Dim TextBox_Value_Edit As TextBox = GridView_Calc.Rows(GridView_Calc.EditIndex).FindControl("TextBox_Value_Edit")
                MySqlComand.Parameters("@VALUE").Value = Convert.ToDecimal(TextBox_Value_Edit.Text)
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@ADDED", SqlDbType.Money))
                Dim TextBox_Added_Edit As TextBox = GridView_Calc.Rows(GridView_Calc.EditIndex).FindControl("TextBox_Added_Edit")
                MySqlComand.Parameters("@ADDED").Value = Convert.ToDecimal(TextBox_Added_Edit.Text)
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@PAID", SqlDbType.Money))
                Dim TextBox_Paid_Edit As TextBox = GridView_Calc.Rows(GridView_Calc.EditIndex).FindControl("TextBox_Paid_Edit")
                MySqlComand.Parameters("@PAID").Value = Convert.ToDecimal(TextBox_Paid_Edit.Text)
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@PENALTIES", SqlDbType.Money))
                Dim TextBox_Penalties_Edit As TextBox = GridView_Calc.Rows(GridView_Calc.EditIndex).FindControl("TextBox_Penalties_Edit")
                MySqlComand.Parameters("@PENALTIES").Value = Convert.ToDecimal(TextBox_Penalties_Edit.Text)
                '---------------------------------------------------------------------------------------

            End If
            '---------------------------------Добавление начисления--------------------------------------
            If type = 3 Then
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@type", SqlDbType.Int))
                MySqlComand.Parameters("@type").Value = 3
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@SERVICE_ID", SqlDbType.Char, 4))
                MySqlComand.Parameters("@SERVICE_ID").Value = SERVICE_ID
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@OCC_ID", SqlDbType.BigInt))
                MySqlComand.Parameters("@OCC_ID").Value = LS
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@OPER_GUID", SqlDbType.VarChar, 50))
                MySqlComand.Parameters("@OPER_GUID").Value = s1
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@APP_GUID", SqlDbType.VarChar, 50))
                MySqlComand.Parameters("@APP_GUID").Value = ConfigurationManager.AppSettings.Item("app_guid").ToString
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@HOST", SqlDbType.VarChar, 50))
                MySqlComand.Parameters("@HOST").Value = HttpContext.Current.Request.UserHostAddress
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@UNITS", SqlDbType.Decimal, 18, 6))
                MySqlComand.Parameters("@UNITS").Value = Convert.ToDecimal(TextBox_Calc_Add_Units.Text)
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@CNTR_UNITS", SqlDbType.Decimal, 18, 6))
                MySqlComand.Parameters("@CNTR_UNITS").Value = Convert.ToDecimal(TextBox_Calc_Add_Cntr_Units.Text)
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@NORM_UNITS", SqlDbType.Decimal, 18, 6))
                MySqlComand.Parameters("@NORM_UNITS").Value = Convert.ToDecimal(TextBox_Calc_Add_Norm_Units.Text)
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@NORM_COEFF", SqlDbType.Decimal, 18, 6))
                MySqlComand.Parameters("@NORM_COEFF").Value = Convert.ToDecimal(TextBox_Calc_Add_Norm_Coeff.Text)
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@USE_NORM_COEFF", SqlDbType.Int))
                MySqlComand.Parameters("@USE_NORM_COEFF").Value = 0
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@PR_DAYS", SqlDbType.Int))
                MySqlComand.Parameters("@PR_DAYS").Value = 0
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@PR_MONTHS", SqlDbType.Int))
                MySqlComand.Parameters("@PR_MONTHS").Value = 0
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@SALDO", SqlDbType.Money))
                MySqlComand.Parameters("@SALDO").Value = Convert.ToDecimal(TextBox_Calc_Add_Saldo.Text)
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@VALUE", SqlDbType.Money))
                MySqlComand.Parameters("@VALUE").Value = Convert.ToDecimal(TextBox_Calc_Add_Value.Text)
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@ADDED", SqlDbType.Money))
                MySqlComand.Parameters("@ADDED").Value = Convert.ToDecimal(TextBox_Calc_Add_Added.Text)
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@PAID", SqlDbType.Money))
                MySqlComand.Parameters("@PAID").Value = Convert.ToDecimal(TextBox_Calc_Add_Paid.Text)
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@PENALTIES", SqlDbType.Money))
                MySqlComand.Parameters("@PENALTIES").Value = Convert.ToDecimal(TextBox_Calc_Add_Penalties.Text)
                '---------------------------------------------------------------------------------------
            End If
            '        '-------------------------------Удаление начисления-------------------------------------------
            If type = 4 Then
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@type", SqlDbType.Int))
                MySqlComand.Parameters("@type").Value = 4
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@OPER_GUID", SqlDbType.VarChar, 50))
                MySqlComand.Parameters("@OPER_GUID").Value = s1
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@APP_GUID", SqlDbType.VarChar, 50))
                MySqlComand.Parameters("@APP_GUID").Value = ConfigurationManager.AppSettings.Item("app_guid").ToString
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@HOST", SqlDbType.VarChar, 50))
                MySqlComand.Parameters("@HOST").Value = HttpContext.Current.Request.UserHostAddress
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@SERVICE_ID", SqlDbType.Char, 4))
                MySqlComand.Parameters("@SERVICE_ID").Value = SERVICE_ID
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@OCC_ID", SqlDbType.BigInt))
                MySqlComand.Parameters("@OCC_ID").Value = LS
                '---------------------------------------------------------------------------------------
            End If
            MyConnection.Open()
            MySqlComand.ExecuteNonQuery()
            MyConnection.Close()
        Catch ex As Exception
            Label_Calc_Error.Text = ex.Message
        End Try
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Нажатие на кнопку "Добавить начисления" (вызов модального окна "Добавить начисления")---------------------------------------------------------------------------------
    Protected Sub Button_Calc_Add_Click(sender As Object, e As EventArgs) Handles Button_Calc_Add.Click
        TextBox_Calc_Add_Norm_Coeff.Text = ""
        TextBox_Calc_Add_Units.Text = ""
        TextBox_Calc_Add_Cntr_Units.Text = ""
        TextBox_Calc_Add_Norm_Units.Text = ""
        TextBox_Calc_Add_Saldo.Text = ""
        TextBox_Calc_Add_Value.Text = ""
        TextBox_Calc_Add_Added.Text = ""
        TextBox_Calc_Add_Paid.Text = ""
        TextBox_Calc_Add_Penalties.Text = ""
        Label_Calc_Add_Error1.Text = ""
        Dim MyConnection As SqlConnection
        Dim MyDataAdapter As SqlDataAdapter
        Dim con1 As String
        Dim s1 As String = FormsAuthentication.Decrypt(Request.Cookies("AppBillingCookie").Value).UserData
        con1 = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
        MyConnection = New SqlConnection(con1)
        MyDataAdapter = New SqlDataAdapter("APP_CALC_COMBO", MyConnection)
        MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@OPER_GUID", SqlDbType.VarChar, 50))
        MyDataAdapter.SelectCommand.Parameters("@OPER_GUID").Value = s1
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@APP_GUID", SqlDbType.VarChar, 50))
        MyDataAdapter.SelectCommand.Parameters("@APP_GUID").Value = ConfigurationManager.AppSettings.Item("app_guid").ToString
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@OCC_ID", SqlDbType.BigInt))
        MyDataAdapter.SelectCommand.Parameters("@OCC_ID").Value = LS
        '---------------------------------------------------------------------------------------
        Dim DS As DataSet
        DS = New DataSet()
        MyDataAdapter.Fill(DS)
        Dim dv0 As DataView = New DataView(DS.Tables(0))
        Dim dv1 As DataView = New DataView(DS.Tables(1))
        'Dim dv2 As DataView = New DataView(DS.Tables(2))
        DropDownList_Calc_Add_Service.DataSource = dv0
        DropDownList_Calc_Add_Service.DataTextField = "Service_Name"
        DropDownList_Calc_Add_Service.DataValueField = "SERVICE_ID"
        DropDownList_Calc_Add_Service.DataBind()

        DropDownList_Calc_Add_Tarif.DataSource = dv1
        DropDownList_Calc_Add_Tarif.DataTextField = "TAR_NAME"
        DropDownList_Calc_Add_Tarif.DataValueField = "TAR_ID"
        DropDownList_Calc_Add_Tarif.DataBind()
        
        UpdatePanel_Calc_Add.Update()
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal_Calc_Add", "$('#myModal_Calc_Add').modal({backdrop: 'static',keyboard: false });", True)
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Нажатие на кнопку "Добавить" модального окна "Добавление начислений"--------------------------------------------------------------------------------------------------
    Protected Sub Button_Calc_Add_Complete_Click(sender As Object, e As EventArgs)
        Try
            Dim MyConnection As SqlConnection
            Dim MyDataAdapter As SqlDataAdapter
            Dim con1 As String
            Dim s1 As String = FormsAuthentication.Decrypt(Request.Cookies("AppBillingCookie").Value).UserData
            con1 = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
            MyConnection = New SqlConnection(con1)
            MyDataAdapter = New SqlDataAdapter("APP_CALC", MyConnection)
            MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            '---------------------------------------------------------------------------------------
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@LS", SqlDbType.BigInt))
            MyDataAdapter.SelectCommand.Parameters("@LS").Value = LS
            '---------------------------------------------------------------------------------------
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@OPER_GUID", SqlDbType.VarChar, 50))
            MyDataAdapter.SelectCommand.Parameters("@OPER_GUID").Value = s1
            '---------------------------------------------------------------------------------------
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@APP_GUID", SqlDbType.VarChar, 50))
            MyDataAdapter.SelectCommand.Parameters("@APP_GUID").Value = ConfigurationManager.AppSettings.Item("app_guid").ToString
            '---------------------------------------------------------------------------------------
            Dim DT As DataTable
            DT = New DataTable()
            MyDataAdapter.Fill(DT)

            Dim is_err As Integer = 0
            For i As Integer = 0 To DT.Rows.Count - 1
                If (DropDownList_Calc_Add_Service.SelectedValue = DT.Rows.Item(i).Item(2)) Then
                    is_err = 1
                End If
            Next

            If (is_err = 0) Then
                CALC_EDIT(DropDownList_Calc_Add_Service.SelectedValue, 3)
            Else
                Label_Calc_Add_Error1.Text = "Такое начисление уже существует"
                UpdatePanel_Calc_Add.Update()
                Exit Sub
            End If
            CALC(LS)
            UpdatePanel_Calc.Update()
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal_Calc_Add", "$('#myModal_Calc_Add').modal('hide');", True)
        Catch ex As Exception
            Label_Calc_Add_Error1.Text = "Ошибка!!!"
            Label_Calc_Add_Error2.ForeColor = Drawing.Color.Blue
            Label_Calc_Add_Error2.Text = ex.Message
            UpdatePanel_Calc_Add.Update()
        End Try
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Клик по картинке "Редактировать" в гриде "Начисления" ----------------------------------------------------------------------------------------------------------------
    Protected Sub GridView_Calc_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles GridView_Calc.RowEditing
        GridView_Calc.EditIndex = e.NewEditIndex
        CALC(LS)
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Клик по картинке "Отменить" в гриде "Начисления"----------------------------------------------------------------------------------------------------------------------
    Protected Sub GridView_Calc_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles GridView_Calc.RowCancelingEdit
        Label_Calc_Error.Text = ""
        GridView_Calc.EditIndex = -1
        CALC(LS)
        GridView_Calc.DataBind()
        UpdatePanel_Calc.Update()
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Клик по картинке "Сохранить" в гриде "Начисления"---------------------------------------------------------------------------------------------------------------------
    Protected Sub GridView_Calc_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles GridView_Calc.RowUpdating
        Try
            Label_Calc_Error.Text = ""
            CALC_EDIT(GridView_Calc.DataKeys.Item(GridView_Calc.EditIndex).Value, 1)
            GridView_Calc.EditIndex = -1
            CALC(LS)
            UpdatePanel_Calc.Update()
        Catch ex As Exception
            Label_Calc_Error.ForeColor = Drawing.Color.Red
            Label_Calc_Error.Text = "!!!Ошибка!!! " + ex.Message
            UpdatePanel_Calc.Update()
            e.Cancel = True
        End Try
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Клик по картинке "Удалить" грида "Начисления" (вызов модального окна "Удаление начисления")---------------------------------------------------------------------------
    Protected Sub GridView_Calc_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles GridView_Calc.RowDeleting
        index_del_calc = e.RowIndex
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal_Calc_delete", "$('#myModal_Calc_delete').modal({backdrop: 'static',keyboard: false });", True)
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Нажатие на кнопку "Удалить" модального окна "Удаление начисления"-----------------------------------------------------------------------------------------------------
    Protected Sub Button_Calc_Dell_Click(sender As Object, e As EventArgs) ' Handles Button_myModal_Arhiv_LS.Click
        Try
            CALC_EDIT(GridView_Calc.DataKeys.Item(index_del_calc).Value, 4)
            CALC(LS)
            UpdatePanel_Calc_delete.Update()
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal_Calc_delete", "$('#myModal_Calc_delete').modal('hide');", True)
        Catch ex As Exception
            Label_Calc_Delete_Error1.Text = "Ошибка"
            Label_Calc_Delete_Error2.ForeColor = Drawing.Color.Blue
            Label_Calc_Delete_Error2.Text = ex.Message
            UpdatePanel_Calc_delete.Update()
        End Try
    End Sub
    '                                                                                                                                                                                              |
    '***********************************************************************************************************************************************************************************************



    '***********************************************************************************************************************************************************************************************
    '|||||||||||||||||||||||| ВКЛАДКА "ИСТОРИЯ НАЧИСЛЕНИЙ" |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    '***********************************************************************************************************************************************************************************************
    '                                                                                                                                                                                              |
    '-------------------------Функция (Заполнение грида "История начислений")-----------------------------------------------------------------------------------------------------------------------
    Protected Sub CALC_HISTORY(LS As Int64)
        Label_Calc_History_Header.Text = "Л/С №" & LS.ToString() & " адрес " & ADDR
        Dim MyConnection As SqlConnection
        Dim MyDataAdapter As SqlDataAdapter
        Dim con1 As String
        Dim s1 As String = FormsAuthentication.Decrypt(Request.Cookies("AppBillingCookie").Value).UserData
        con1 = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
        MyConnection = New SqlConnection(con1)
        MyDataAdapter = New SqlDataAdapter("APP_CALC_HISTORY", MyConnection)
        MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@LS", SqlDbType.BigInt))
        MyDataAdapter.SelectCommand.Parameters("@LS").Value = LS
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@OPER_GUID", SqlDbType.VarChar, 50))
        MyDataAdapter.SelectCommand.Parameters("@OPER_GUID").Value = s1
        '---------------------------------------------------------------------------------------
        MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@APP_GUID", SqlDbType.VarChar, 50))
        MyDataAdapter.SelectCommand.Parameters("@APP_GUID").Value = ConfigurationManager.AppSettings.Item("app_guid").ToString
        '---------------------------------------------------------------------------------------
        Dim DS As DataSet
        DS = New DataSet()
        MyDataAdapter.Fill(DS)
        GridView_Calc_History.DataSource = DS.Tables(0).DefaultView
        GridView_Calc_History.AutoGenerateColumns = False
        GridView_Calc_History.Visible = True
        GridView_Calc_History.DataBind()
        'Dim dv1 As DataView = New DataView(DS.Tables(1))
        'For i As Integer = 0 To GridView_Counters.Rows.Count - 1
        '    Dim DropDownList_Counters_Service_Edit As DropDownList = GridView_Counters.Rows(i).FindControl("DropDownList_Counters_Service_Edit")
        '    If IsNothing(DropDownList_Counters_Service_Edit) Then
        '    Else
        '        DropDownList_Counters_Service_Edit.DataSource = dv1
        '        DropDownList_Counters_Service_Edit.DataTextField = "ID_Service"
        '        DropDownList_Counters_Service_Edit.DataValueField = "ID_Service"
        '        DropDownList_Counters_Service_Edit.DataBind()
        '        DropDownList_Counters_Service_Edit.SelectedValue = DS.Tables(0).Rows.Item(i).Item(2)
        '    End If
        'Next
        UpdatePanel_Calc_History.Update()
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Функция "РЕДАКТИРОВАНИЕ, ДОБАВЛЕНИЕ, УДАЛЕНИЕ ИСТОРИИ НАЧИСЛЕНИЙ"-----------------------------------------------------------------------------------------------------
    Protected Sub CALC_HISTORY_EDIT(SERVICE_ID As String, FT_ID As Integer, type As Integer)
        Try
            Dim MyConnection As SqlConnection
            Dim MySqlComand As SqlCommand
            Dim con1 As String
            Dim s1 As String = FormsAuthentication.Decrypt(Request.Cookies("AppBillingCookie").Value).UserData
            con1 = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
            MyConnection = New SqlConnection(con1)
            MySqlComand = New SqlCommand("APP_CALC_HISTORY_EDIT", MyConnection)
            MySqlComand.CommandType = CommandType.StoredProcedure
            '---------------------------------Редактирование истории начислений-------------------------
            If type = 1 Then
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@type", SqlDbType.Int))
                MySqlComand.Parameters("@type").Value = 1
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@SERVICE_ID", SqlDbType.Char, 4))
                MySqlComand.Parameters("@SERVICE_ID").Value = SERVICE_ID
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@OCC_ID", SqlDbType.BigInt))
                MySqlComand.Parameters("@OCC_ID").Value = LS
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@OPER_GUID", SqlDbType.VarChar, 50))
                MySqlComand.Parameters("@OPER_GUID").Value = s1
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@APP_GUID", SqlDbType.VarChar, 50))
                MySqlComand.Parameters("@APP_GUID").Value = ConfigurationManager.AppSettings.Item("app_guid").ToString
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@HOST", SqlDbType.VarChar, 50))
                MySqlComand.Parameters("@HOST").Value = HttpContext.Current.Request.UserHostAddress
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@UNITS", SqlDbType.Decimal, 18, 6))
                Dim TextBox_Calc_History_Units_Edit As TextBox = GridView_Calc_History.Rows(GridView_Calc_History.EditIndex).FindControl("TextBox_Calc_History_Units_Edit")
                MySqlComand.Parameters("@UNITS").Value = Convert.ToDecimal(TextBox_Calc_History_Units_Edit.Text)
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@CNTR_UNITS", SqlDbType.Decimal, 18, 6))
                Dim TextBox_Calc_History_Cntr_Units_Edit As TextBox = GridView_Calc_History.Rows(GridView_Calc_History.EditIndex).FindControl("TextBox_Calc_History_Cntr_Units_Edit")
                MySqlComand.Parameters("@CNTR_UNITS").Value = Convert.ToDecimal(TextBox_Calc_History_Cntr_Units_Edit.Text)
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@NORM_UNITS", SqlDbType.Decimal, 18, 6))
                Dim TextBox_Calc_History_Norm_Units_Edit As TextBox = GridView_Calc_History.Rows(GridView_Calc_History.EditIndex).FindControl("TextBox_Calc_History_Norm_Units_Edit")
                MySqlComand.Parameters("@NORM_UNITS").Value = Convert.ToDecimal(TextBox_Calc_History_Norm_Units_Edit.Text)
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@NORM_COEFF", SqlDbType.Decimal, 18, 6))
                Dim TextBox_Calc_History_Norm_Coeff_Edit As TextBox = GridView_Calc_History.Rows(GridView_Calc_History.EditIndex).FindControl("TextBox_Calc_History_Norm_Coeff_Edit")
                MySqlComand.Parameters("@NORM_COEFF").Value = Convert.ToDecimal(TextBox_Calc_History_Norm_Coeff_Edit.Text)
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@SALDO", SqlDbType.Money))
                Dim TextBox_Calc_History_Saldo_Edit As TextBox = GridView_Calc_History.Rows(GridView_Calc_History.EditIndex).FindControl("TextBox_Calc_History_Saldo_Edit")
                MySqlComand.Parameters("@SALDO").Value = Convert.ToDecimal(TextBox_Calc_History_Saldo_Edit.Text)
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@VALUE", SqlDbType.Money))
                Dim TextBox_Calc_History_Value_Edit As TextBox = GridView_Calc_History.Rows(GridView_Calc_History.EditIndex).FindControl("TextBox_Calc_History_Value_Edit")
                MySqlComand.Parameters("@VALUE").Value = Convert.ToDecimal(TextBox_Calc_History_Value_Edit.Text)
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@ADDED", SqlDbType.Money))
                Dim TextBox_Calc_History_Added_Edit As TextBox = GridView_Calc_History.Rows(GridView_Calc_History.EditIndex).FindControl("TextBox_Calc_History_Added_Edit")
                MySqlComand.Parameters("@ADDED").Value = Convert.ToDecimal(TextBox_Calc_History_Added_Edit.Text)
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@PAID", SqlDbType.Money))
                Dim TextBox_Calc_History_Paid_Edit As TextBox = GridView_Calc_History.Rows(GridView_Calc_History.EditIndex).FindControl("TextBox_Calc_History_Paid_Edit")
                MySqlComand.Parameters("@PAID").Value = Convert.ToDecimal(TextBox_Calc_History_Paid_Edit.Text)
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@PENALTIES", SqlDbType.Money))
                Dim TextBox_Calc_History_Penalties_Edit As TextBox = GridView_Calc_History.Rows(GridView_Calc_History.EditIndex).FindControl("TextBox_Calc_History_Penalties_Edit")
                MySqlComand.Parameters("@PENALTIES").Value = Convert.ToDecimal(TextBox_Calc_History_Penalties_Edit.Text)
                '---------------------------------------------------------------------------------------

            End If
            '---------------------------------Добавление в историю начислений---------------------------
            If type = 3 Then
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@type", SqlDbType.Int))
                MySqlComand.Parameters("@type").Value = 3
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@SERVICE_ID", SqlDbType.Char, 4))
                MySqlComand.Parameters("@SERVICE_ID").Value = DropDownList_Calc_History_Add_Service.SelectedValue 'SERVICE_ID
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@OCC_ID", SqlDbType.BigInt))
                MySqlComand.Parameters("@OCC_ID").Value = LS
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@OPER_GUID", SqlDbType.VarChar, 50))
                MySqlComand.Parameters("@OPER_GUID").Value = s1
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@APP_GUID", SqlDbType.VarChar, 50))
                MySqlComand.Parameters("@APP_GUID").Value = ConfigurationManager.AppSettings.Item("app_guid").ToString
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@HOST", SqlDbType.VarChar, 50))
                MySqlComand.Parameters("@HOST").Value = HttpContext.Current.Request.UserHostAddress
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@TAR_ID", SqlDbType.Int))
                MySqlComand.Parameters("@TAR_ID").Value = DropDownList_Calc_History_Add_Tar_ID.SelectedValue
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@UNITS", SqlDbType.Decimal, 18, 6))
                MySqlComand.Parameters("@UNITS").Value = Convert.ToDecimal(TextBox_Calc_History_Add_Units.Text)
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@CNTR_UNITS", SqlDbType.Decimal, 18, 6))
                MySqlComand.Parameters("@CNTR_UNITS").Value = Convert.ToDecimal(TextBox_Calc_History_Add_Cntr_Units.Text)
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@NORM_UNITS", SqlDbType.Decimal, 18, 6))
                MySqlComand.Parameters("@NORM_UNITS").Value = Convert.ToDecimal(TextBox_Calc_History_Add_Norm_Units.Text)
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@NORM_COEFF", SqlDbType.Decimal, 18, 6))
                MySqlComand.Parameters("@NORM_COEFF").Value = 0
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@FT_ID", SqlDbType.Int))
                MySqlComand.Parameters("@FT_ID").Value = DropDownList_Calc_History_Add_FT.SelectedValue '15
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@CALC_STATUS", SqlDbType.Int))
                MySqlComand.Parameters("@CALC_STATUS").Value = 1
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@CNTR_STATUS", SqlDbType.Int))
                MySqlComand.Parameters("@CNTR_STATUS").Value = 0
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@SALDO", SqlDbType.Money))
                MySqlComand.Parameters("@SALDO").Value = Convert.ToDecimal(TextBox_Calc_History_Add_Saldo.Text)
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@VALUE", SqlDbType.Money))
                MySqlComand.Parameters("@VALUE").Value = Convert.ToDecimal(TextBox_Calc_History_Add_Value.Text)
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@ADDED", SqlDbType.Money))
                MySqlComand.Parameters("@ADDED").Value = Convert.ToDecimal(TextBox_Calc_History_Add_Added.Text)
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@PAID", SqlDbType.Money))
                MySqlComand.Parameters("@PAID").Value = Convert.ToDecimal(TextBox_Calc_History_Add_Paid.Text)
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@PENALTIES", SqlDbType.Money))
                MySqlComand.Parameters("@PENALTIES").Value = Convert.ToDecimal(TextBox_Calc_History_Add_Penalties.Text)
                '---------------------------------------------------------------------------------------
            End If
            '-------------------------------Удаление из истории начислений------------------------------
            If type = 4 Then
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@type", SqlDbType.Int))
                MySqlComand.Parameters("@type").Value = 4
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@OPER_GUID", SqlDbType.VarChar, 50))
                MySqlComand.Parameters("@OPER_GUID").Value = s1
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@APP_GUID", SqlDbType.VarChar, 50))
                MySqlComand.Parameters("@APP_GUID").Value = ConfigurationManager.AppSettings.Item("app_guid").ToString
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@HOST", SqlDbType.VarChar, 50))
                MySqlComand.Parameters("@HOST").Value = HttpContext.Current.Request.UserHostAddress
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@SERVICE_ID", SqlDbType.Char, 4))
                MySqlComand.Parameters("@SERVICE_ID").Value = SERVICE_ID
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@FT_ID", SqlDbType.Int))
                MySqlComand.Parameters("@FT_ID").Value = FT_ID
                '---------------------------------------------------------------------------------------
                MySqlComand.Parameters.Add(New SqlParameter("@OCC_ID", SqlDbType.BigInt))
                MySqlComand.Parameters("@OCC_ID").Value = LS
                '---------------------------------------------------------------------------------------
            End If
            MyConnection.Open()
            MySqlComand.ExecuteNonQuery()
            MyConnection.Close()
        Catch ex As Exception
            Label_Calc_History_Error.Text = ex.Message
        End Try
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Нажатие на кнопку "Добавить в историю начислений" (вызов модального окна "Добавить в историю начислений")-------------------------------------------------------------
    Protected Sub Button_Calc_History_Add_Click(sender As Object, e As EventArgs) Handles Button_Calc_History_Add.Click
        Try
            TextBox_Calc_History_Add_Norm_Coeff.Text = ""
            TextBox_Calc_History_Add_Units.Text = ""
            TextBox_Calc_History_Add_Cntr_Units.Text = ""
            TextBox_Calc_History_Add_Norm_Units.Text = ""
            TextBox_Calc_History_Add_Saldo.Text = ""
            TextBox_Calc_History_Add_Value.Text = ""
            TextBox_Calc_History_Add_Added.Text = ""
            TextBox_Calc_History_Add_Paid.Text = ""
            TextBox_Calc_History_Add_Penalties.Text = ""
            Label_Calc_History_Add_Error1.Text = ""
            Dim MyConnection As SqlConnection
            Dim MyDataAdapter As SqlDataAdapter
            Dim con1 As String
            Dim s1 As String = FormsAuthentication.Decrypt(Request.Cookies("AppBillingCookie").Value).UserData
            con1 = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
            MyConnection = New SqlConnection(con1)
            MyDataAdapter = New SqlDataAdapter("APP_CALC_HISTORY_COMBO", MyConnection)
            MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            '---------------------------------------------------------------------------------------
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@OPER_GUID", SqlDbType.VarChar, 50))
            MyDataAdapter.SelectCommand.Parameters("@OPER_GUID").Value = s1
            '---------------------------------------------------------------------------------------
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@APP_GUID", SqlDbType.VarChar, 50))
            MyDataAdapter.SelectCommand.Parameters("@APP_GUID").Value = ConfigurationManager.AppSettings.Item("app_guid").ToString
            '---------------------------------------------------------------------------------------
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@OCC_ID", SqlDbType.BigInt))
            MyDataAdapter.SelectCommand.Parameters("@OCC_ID").Value = LS
            '---------------------------------------------------------------------------------------
            Dim DS As DataSet
            DS = New DataSet()
            MyDataAdapter.Fill(DS)
            Dim dv0 As DataView = New DataView(DS.Tables(0))
            Dim dv1 As DataView = New DataView(DS.Tables(1))
            Dim dv2 As DataView = New DataView(DS.Tables(2))
            DropDownList_Calc_History_Add_FT.DataSource = dv0
            DropDownList_Calc_History_Add_FT.DataTextField = "FT_DATE"
            DropDownList_Calc_History_Add_FT.DataValueField = "ID"
            DropDownList_Calc_History_Add_FT.DataBind()
            DropDownList_Calc_History_Add_Service.DataSource = dv1
            DropDownList_Calc_History_Add_Service.DataTextField = "Service_Name"
            DropDownList_Calc_History_Add_Service.DataValueField = "SERVICE_ID"
            DropDownList_Calc_History_Add_Service.DataBind()
            DropDownList_Calc_History_Add_Tar_ID.DataSource = dv2
            DropDownList_Calc_History_Add_Tar_ID.DataTextField = "TAR_NAME"
            DropDownList_Calc_History_Add_Tar_ID.DataValueField = "TAR_ID"
            DropDownList_Calc_History_Add_Tar_ID.DataBind()
            MyConnection = New SqlConnection(con1)
            MyDataAdapter = New SqlDataAdapter("APP_CALC_HISTORY_PROVERKA_FT", MyConnection)
            MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            '---------------------------------------------------------------------------------------
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@OCC_ID", SqlDbType.BigInt))
            MyDataAdapter.SelectCommand.Parameters("@OCC_ID").Value = LS
            '---------------------------------------------------------------------------------------
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@OPER_GUID", SqlDbType.VarChar, 50))
            MyDataAdapter.SelectCommand.Parameters("@OPER_GUID").Value = s1
            '---------------------------------------------------------------------------------------
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@APP_GUID", SqlDbType.VarChar, 50))
            MyDataAdapter.SelectCommand.Parameters("@APP_GUID").Value = ConfigurationManager.AppSettings.Item("app_guid").ToString
            '---------------------------------------------------------------------------------------
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@FT_ID", SqlDbType.Int))
            MyDataAdapter.SelectCommand.Parameters("@FT_ID").Value = DropDownList_Calc_History_Add_FT.SelectedValue
            '---------------------------------------------------------------------------------------
            DS = New DataSet()
            MyDataAdapter.Fill(DS)
            Dim dv3 As DataView = New DataView(DS.Tables(0))
            DropDownList_Calc_History_Add_Service.DataSource = dv3
            DropDownList_Calc_History_Add_Service.DataTextField = "Service_Name"
            DropDownList_Calc_History_Add_Service.DataValueField = "SERVICE_ID"
            DropDownList_Calc_History_Add_Service.DataBind()
            UpdatePanel_Calc_History_Add.Update()
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal_Calc_History_Add", "$('#myModal_Calc_History_Add').modal({backdrop: 'static',keyboard: false });", True)
        Catch ex As Exception
            Label_Calc_History_Add_Error1.Text = "Ошибка"
            Label_Calc_History_Add_Error2.ForeColor = Drawing.Color.Blue
            Label_Calc_History_Add_Error2.Text = ex.Message
            UpdatePanel_Calc_History_Add.Update()
        End Try
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Нажатие на кнопку "Добавить" модального окна "Добавить в историю начислений"------------------------------------------------------------------------------------------
    Protected Sub Button_Calc_History_Add_Complete_Click(sender As Object, e As EventArgs)
        Try
            Dim MyConnection As SqlConnection
            Dim MyDataAdapter As SqlDataAdapter
            Dim con1 As String
            Dim s1 As String = FormsAuthentication.Decrypt(Request.Cookies("AppBillingCookie").Value).UserData
            con1 = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
            MyConnection = New SqlConnection(con1)
            MyDataAdapter = New SqlDataAdapter("APP_CALC_HISTORY", MyConnection)
            MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            '---------------------------------------------------------------------------------------
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@LS", SqlDbType.BigInt))
            MyDataAdapter.SelectCommand.Parameters("@LS").Value = LS
            '---------------------------------------------------------------------------------------
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@OPER_GUID", SqlDbType.VarChar, 50))
            MyDataAdapter.SelectCommand.Parameters("@OPER_GUID").Value = s1
            '---------------------------------------------------------------------------------------
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@APP_GUID", SqlDbType.VarChar, 50))
            MyDataAdapter.SelectCommand.Parameters("@APP_GUID").Value = ConfigurationManager.AppSettings.Item("app_guid").ToString
            '---------------------------------------------------------------------------------------
            Dim DT As DataTable
            DT = New DataTable()
            MyDataAdapter.Fill(DT)

            Dim is_err As Integer = 0
            For i As Integer = 0 To DT.Rows.Count - 1
                If (DropDownList_Calc_History_Add_FT.SelectedValue = DT.Rows.Item(i).Item(2) And DropDownList_Calc_History_Add_Service.SelectedValue = DT.Rows.Item(i).Item(3)) Then
                    is_err = 1
                End If
            Next

            If (is_err = 0) Then
                CALC_HISTORY_EDIT("", 0, 3)
            Else
                Label_Calc_History_Add_Error1.Text = "Такое начисление уже существует"
                UpdatePanel_Calc_History_Add.Update()
                Exit Sub
            End If
            CALC_HISTORY(LS)
            UpdatePanel_Calc_History.Update()
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal_Calc_History_Add", "$('#myModal_Calc_History_Add').modal('hide');", True)
        Catch ex As Exception
            Label_Calc_History_Add_Error1.Text = "Ошибка"
            Label_Calc_History_Add_Error2.ForeColor = Drawing.Color.Blue
            Label_Calc_History_Add_Error2.Text = ex.Message
            UpdatePanel_Calc_History_Add.Update()
        End Try
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Клик по картинке "Редактировать" в гриде "История начислений" --------------------------------------------------------------------------------------------------------
    Protected Sub GridView_Calc_History_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles GridView_Calc_History.RowEditing
        GridView_Calc_History.EditIndex = e.NewEditIndex
        CALC_HISTORY(LS)
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Клик по картинке "Отменить" в гриде "История начислений"--------------------------------------------------------------------------------------------------------------
    Protected Sub GridView_Calc_History_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles GridView_Calc_History.RowCancelingEdit
        Label_Calc_History_Error.Text = ""
        GridView_Calc_History.EditIndex = -1
        CALC_HISTORY(LS)
        GridView_Calc_History.DataBind()
        UpdatePanel_Calc_History.Update()
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Клик по картинке "Сохранить" в гриде "История начислений"-------------------------------------------------------------------------------------------------------------
    Protected Sub GridView_Calc_History_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles GridView_Calc_History.RowUpdating
        Try
            Label_Calc_History_Error.Text = ""
            CALC_HISTORY_EDIT(GridView_Calc_History.DataKeys.Item(GridView_Calc_History.EditIndex).Item(0), GridView_Calc_History.DataKeys.Item(GridView_Calc_History.EditIndex).Item(1), 1)
            GridView_Calc_History.EditIndex = -1
            CALC_HISTORY(LS)
            UpdatePanel_Calc_History.Update()
        Catch ex As Exception
            Label_Calc_History_Error.ForeColor = Drawing.Color.Red
            Label_Calc_History_Error.Text = "!!!Ошибка!!! " + ex.Message
            UpdatePanel_Calc_History.Update()
            e.Cancel = True
        End Try
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Клик по картинке "Удалить" грида "История начислений" (вызов модального окна "Удаление из истории начислений")--------------------------------------------------------
    Protected Sub GridView_Calc_History_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles GridView_Calc_History.RowDeleting
        index_del_calc_history = e.RowIndex
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal_Calc_History_delete", "$('#myModal_Calc_History_delete').modal({backdrop: 'static',keyboard: false });", True)
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Нажатие на кнопку "Удалить" модального окна "Удаление из истории начислений"------------------------------------------------------------------------------------------
    Protected Sub Button_Calc_History_Dell_Click(sender As Object, e As EventArgs)
        'MsgBox(GridView_Calc_History.DataKeys.Item(index_del_calc_history).Item(0))
        'MsgBox(GridView_Calc_History.DataKeys.Item(index_del_calc_history).Item(1))
        Try
            CALC_HISTORY_EDIT(GridView_Calc_History.DataKeys.Item(index_del_calc_history).Item(0), GridView_Calc_History.DataKeys.Item(index_del_calc_history).Item(1), 4)
            CALC_HISTORY(LS)
            UpdatePanel_Calc_History_delete.Update()
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal_Calc_History_delete", "$('#myModal_Calc_History_delete').modal('hide');", True)
        Catch ex As Exception
            Label_Calc_History_Delete_Error1.Text = "Ошибка"
            Label_Calc_History_Delete_Error2.ForeColor = Drawing.Color.Blue
            Label_Calc_History_Delete_Error2.Text = ex.Message
            UpdatePanel_Calc_History_delete.Update()
        End Try
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Выбор финансового периода в модальном окне "Удаление из истории начислений"-------------------------------------------------------------------------------------------
    Protected Sub DropDownList_Calc_History_Add_FT_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            Dim MyConnection As SqlConnection
            Dim MyDataAdapter As SqlDataAdapter
            Dim con1 As String
            Dim s1 As String = FormsAuthentication.Decrypt(Request.Cookies("AppBillingCookie").Value).UserData
            con1 = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
            MyConnection = New SqlConnection(con1)
            MyDataAdapter = New SqlDataAdapter("APP_CALC_HISTORY_PROVERKA_FT", MyConnection)
            MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            '---------------------------------------------------------------------------------------
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@OCC_ID", SqlDbType.BigInt))
            MyDataAdapter.SelectCommand.Parameters("@OCC_ID").Value = LS
            '---------------------------------------------------------------------------------------
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@OPER_GUID", SqlDbType.VarChar, 50))
            MyDataAdapter.SelectCommand.Parameters("@OPER_GUID").Value = s1
            '---------------------------------------------------------------------------------------
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@APP_GUID", SqlDbType.VarChar, 50))
            MyDataAdapter.SelectCommand.Parameters("@APP_GUID").Value = ConfigurationManager.AppSettings.Item("app_guid").ToString
            '---------------------------------------------------------------------------------------
            MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@FT_ID", SqlDbType.Int))
            MyDataAdapter.SelectCommand.Parameters("@FT_ID").Value = DropDownList_Calc_History_Add_FT.SelectedValue
            '---------------------------------------------------------------------------------------
            Dim DS As DataSet
            DS = New DataSet()
            MyDataAdapter.Fill(DS)
            Dim dv0 As DataView = New DataView(DS.Tables(0))
            DropDownList_Calc_History_Add_Service.DataSource = dv0
            DropDownList_Calc_History_Add_Service.DataTextField = "Service_Name"
            DropDownList_Calc_History_Add_Service.DataValueField = "SERVICE_ID"
            DropDownList_Calc_History_Add_Service.DataBind()
            UpdatePanel_Calc_History_Add.Update()
        Catch ex As Exception
            Label_Calc_History_Add_Error1.Text = "Ошибка"
            Label_Calc_History_Add_Error2.ForeColor = Drawing.Color.Blue
            Label_Calc_History_Add_Error2.Text = ex.Message
            UpdatePanel_Calc_History_Add.Update()
        End Try
    End Sub
    '                                                                                                                                                                                              |
    '***********************************************************************************************************************************************************************************************




    '***********************************************************************************************************************************************************************************************
    '|||||||||||||||||||||||| ВКЛАДКА "НОВАЯ ВКЛАДКА" |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    '***********************************************************************************************************************************************************************************************
    '                                                                                                                                                                                              |
    '-------------------------Функция (Заполнение грида "Новая вкладка")----------------------------------------------------------------------------------------------------------------------------
    Protected Sub NEW_PROCEDURE(LS As Int64)
        Label_New_Header.Text = "Л/С №" & LS.ToString() & " адрес " & ADDR
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Нажатие на кнопку "Добавить в новую вкладку" (вызов модального окна "Добавить в историю начислений")------------------------------------------------------------------
    Protected Sub Button_New_Add_Click(sender As Object, e As EventArgs)
        Label_New_Add_Error1.Text = ""
        'Dim MyConnection As SqlConnection
        'Dim MyDataAdapter As SqlDataAdapter
        'Dim con1 As String
        'Dim s1 As String = FormsAuthentication.Decrypt(Request.Cookies("AppBillingCookie").Value).UserData
        'con1 = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
        'MyConnection = New SqlConnection(con1)
        'MyDataAdapter = New SqlDataAdapter("APP_NEW_COMBO", MyConnection)
        'MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
        ''---------------------------------------------------------------------------------------
        'MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@OPER_GUID", SqlDbType.VarChar, 50))
        'MyDataAdapter.SelectCommand.Parameters("@OPER_GUID").Value = s1
        ''---------------------------------------------------------------------------------------
        'MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@APP_GUID", SqlDbType.VarChar, 50))
        'MyDataAdapter.SelectCommand.Parameters("@APP_GUID").Value = ConfigurationManager.AppSettings.Item("app_guid").ToString
        ''---------------------------------------------------------------------------------------
        'MyDataAdapter.SelectCommand.Parameters.Add(New SqlParameter("@OCC_ID", SqlDbType.BigInt))
        'MyDataAdapter.SelectCommand.Parameters("@OCC_ID").Value = LS
        ''---------------------------------------------------------------------------------------

        'Dim DS As DataSet
        'DS = New DataSet()
        'MyDataAdapter.Fill(DS)
        'Dim dv0 As DataView = New DataView(DS.Tables(0))
        'Dim dv1 As DataView = New DataView(DS.Tables(1))
        'Dim dv2 As DataView = New DataView(DS.Tables(2))
        'DropDownList_Calc_History_Add_FT.DataSource = dv0
        'DropDownList_Calc_History_Add_FT.DataTextField = "FT_DATE"
        'DropDownList_Calc_History_Add_FT.DataValueField = "ID"
        'DropDownList_Calc_History_Add_FT.DataBind()

        'DropDownList_Calc_History_Add_Service.DataSource = dv1
        'DropDownList_Calc_History_Add_Service.DataTextField = "Service_Name"
        'DropDownList_Calc_History_Add_Service.DataValueField = "SERVICE_ID"
        'DropDownList_Calc_History_Add_Service.DataBind()

        'DropDownList_Calc_History_Add_Tar_ID.DataSource = dv2
        'DropDownList_Calc_History_Add_Tar_ID.DataTextField = "TAR_NAME"
        'DropDownList_Calc_History_Add_Tar_ID.DataValueField = "TAR_ID"
        'DropDownList_Calc_History_Add_Tar_ID.DataBind()

        'UpdatePanel_New_Add.Update()
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal_New_Add", "$('#myModal_New_Add').modal({backdrop: 'static',keyboard: false });", True)
    End Sub
    '                                                                                                                                                                                              |
    '-------------------------Нажатие на кнопку "Добавить" модального окна "Добавить в новую вкладку"-----------------------------------------------------------------------------------------------
    Protected Sub Button_New_Add_Complete_ServerClick(sender As Object, e As EventArgs)
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal_New_Add", "$('#myModal_New_Add').modal('hide');", True)
    End Sub
    '                                                                                                                                                                                              |
    '***********************************************************************************************************************************************************************************************

End Class