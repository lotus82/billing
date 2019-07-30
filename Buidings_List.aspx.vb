Imports System.Data.SqlClient
Imports System.Drawing

Public Class Buidings_List
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Label_danger.Text = ""

        If IsPostBack Then
            Exit Sub
        End If

        SqlDataSourceBuildings.SelectParameters.Add("user_guid", FormsAuthentication.Decrypt(Request.Cookies("AppBillingCookie").Value).UserData)
        SqlDataSourceFlats.SelectParameters.Add("user_guid", FormsAuthentication.Decrypt(Request.Cookies("AppBillingCookie").Value).UserData)
        Address(1)

    End Sub

    Protected Function Address(type As Integer)

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
        Return Nothing
        Exit Function
err:

    End Function
    Protected Sub GridViewBuildings_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles GridViewBuildings.RowEditing
        GridViewBuildings.EditIndex = e.NewEditIndex
        GridViewBuildings.SelectRow(GridViewBuildings.EditIndex)
    End Sub

    Protected Sub GridViewBuildings_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles GridViewBuildings.RowUpdating
        SqlDataSourceBuildings.UpdateCommand = "execute App_Bldn_Edit 1,@ID_build,@Build,NULL,@ENTRANCES,@FLOORS,@INDEX_ID,@street_id,@ID_TECH,@FIAS_GUD,@OPER_GUID,@APP_GUID,@HOST"

        SqlDataSourceBuildings.UpdateParameters.Add("street_id", DropDownList_UL.SelectedValue)
        SqlDataSourceBuildings.UpdateParameters.Add("OPER_GUID", FormsAuthentication.Decrypt(Request.Cookies("AppBillingCookie").Value).UserData)
        SqlDataSourceBuildings.UpdateParameters.Add("APP_GUID", ConfigurationManager.AppSettings.Item("app_guid").ToString)
        SqlDataSourceBuildings.UpdateParameters.Add("HOST", HttpContext.Current.Request.UserHostAddress)
    End Sub

    Protected Sub GridViewBuildings_RowUpdated(sender As Object, e As GridViewUpdatedEventArgs) Handles GridViewBuildings.RowUpdated
        If e.Exception IsNot Nothing Then
            Label_danger.Text = e.Exception.Message
            e.ExceptionHandled = True
            Dim message As String = e.Exception.Message
            ErrMsg(message)
           
        End If
    End Sub

    Protected Sub ButtonClosed_Click(sender As Object, e As ImageClickEventArgs) Handles ButtonClosed.Click
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Modal_Flats", "$('#Modal_Flats').modal('hide');", True)
        LabelChangeError.Text = ""
    End Sub
    Protected Sub GridViewBuildings_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridViewBuildings.SelectedIndexChanged

        For Each row As GridViewRow In GridViewBuildings.Rows
            If row.RowIndex = GridViewBuildings.SelectedIndex Then
                row.BackColor = ColorTranslator.FromHtml("#A1DCF2")
                Dim row0 As GridViewRow = GridViewBuildings.Rows(row.RowIndex)
                Dim txtName As Label = TryCast(row0.FindControl("LabelBuild"), Label)
                If txtName IsNot Nothing Then
                    lblModalTitle.Text = "Список квартир в доме <br/> " & DropDownList_UL.SelectedItem.Text & " д. " & txtName.Text
                End If

            Else
                row.BackColor = ColorTranslator.FromHtml("#FFFFFF")
            End If
        Next



    End Sub

    Protected Sub GridViewBuildings_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridViewBuildings.RowCommand
        If e.CommandName = "Select" Then
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Modal_Flats", "$('#Modal_Flats').modal({backdrop: 'static',keyboard: false });", True)
            upModal.Update()
        End If
    End Sub

    Protected Sub ImageButton1_Click(sender As Object, e As ImageClickEventArgs)
        upModal.Update()
    End Sub

    Protected Sub ButtonAddOcc_ServerClick(sender As Object, e As EventArgs)
        Dim errText As String = ""
        LabelChangeError.Text = ""
        If String.IsNullOrEmpty(TextBoxFlat1.Text) Then
            LabelChangeError.Text = "Введите начальный диапазон!"

            upModal.Update()
            Exit Sub
        ElseIf String.IsNullOrEmpty(TextBoxFlat2.Text) Then
            LabelChangeError.Text = "Введите конечный диапазон !"
            upModal.Update()
            Exit Sub
        End If

        Dim con As String
        con = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
        Try


            Using MyConnection = New SqlConnection(con)
                Dim cm As New SqlCommand("APP_Flat_Edit", MyConnection)
                Dim dr As SqlDataReader
                cm.CommandType = CommandType.StoredProcedure
                Dim prmtype = New SqlParameter("@type", SqlDbType.Int)
                prmtype.Value = 1
                cm.Parameters.Add(prmtype)
                '-----------------------------------------------------------------------
                Dim prmid_flats = New SqlParameter("@id_flats", SqlDbType.Int)
                prmid_flats.Value = 0
                cm.Parameters.Add(prmid_flats)


                '--------------------------------------------------------
                Dim prmFLAT = New SqlParameter("@FLAT", SqlDbType.NVarChar, 50)
                prmFLAT.Value = ""
                cm.Parameters.Add(prmFLAT)

                '--------------------------------------------------------
                Dim prmFLT1 = New SqlParameter("@flt1", SqlDbType.Int)
                prmFLT1.Value = TextBoxFlat1.Text
                cm.Parameters.Add(prmFLT1)
                '--------------------------------------------------------
                Dim prmFLT2 = New SqlParameter("@flt2", SqlDbType.Int)
                prmFLT2.Value = TextBoxFlat2.Text
                cm.Parameters.Add(prmFLT2)
                '--------------------------------------------------------
                Dim prmENTRANCE = New SqlParameter("@ENTRANCE", SqlDbType.Int)
                prmENTRANCE.Value = 1
                cm.Parameters.Add(prmENTRANCE)
                '--------------------------------------------------------
                Dim prmFLOORFLAT = New SqlParameter("@FLOORFLAT", SqlDbType.Int)
                prmFLOORFLAT.Value = 1
                cm.Parameters.Add(prmFLOORFLAT)
                '--------------------------------------------------------
                Dim prmID_BUILD = New SqlParameter("@ID_BUILD", SqlDbType.Int)
                prmID_BUILD.Value = GridViewBuildings.SelectedValue
                cm.Parameters.Add(prmID_BUILD)
                '--------------------------------------------------------
                Dim prmOPER_GUID = New SqlParameter("@OPER_GUID", SqlDbType.NVarChar, 50)
                prmOPER_GUID.Value = FormsAuthentication.Decrypt(Request.Cookies("AppBillingCookie").Value).UserData
                cm.Parameters.Add(prmOPER_GUID)

                '--------------------------------------------------------
                Dim prmSS = New SqlParameter("@app_guid", SqlDbType.NVarChar, 50)
                prmSS.Value = ConfigurationManager.AppSettings.Item("app_guid").ToString
                cm.Parameters.Add(prmSS)
                '----------------------------------------
                Dim prmIP = New SqlParameter("@HOST", SqlDbType.NVarChar, 50)
                prmIP.Value = HttpContext.Current.Request.UserHostAddress
                cm.Parameters.Add(prmIP)

                MyConnection.Open()
                dr = cm.ExecuteReader


                If dr.HasRows = True Then

                    While dr.Read()


                        errText = errText & dr(0) & "<br/>"

                    End While
                End If

                dr.Close()
            End Using

            GridViewFlats.DataBind()
            upModal.Update()

            If errText.Length > 1 Then
                ErrMsg(errText)
            End If

        Catch ex As Exception
            ErrMsg(ex.Message)
            upModal.Update()
        End Try

    End Sub

    Protected Sub GridViewFlats_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles GridViewFlats.RowUpdating
        '(@type int,@id_flats int,@FLAT varchar(50),@flt1 int,@flt2 int,@ENTRANCE INT,@FLOORFLAT int,@ID_BUILD int,@OPER_GUID varchar(100),@APP_GUID varchar(100),@HOST varchar(50))

        SqlDataSourceFlats.UpdateCommand = "execute APP_Flat_Edit 2,@id_flats,@FLAT,0,0,@ENTRANCE,@FLOORFLAT,@ID_BUILD,@OPER_GUID,@APP_GUID,@HOST"

        SqlDataSourceFlats.UpdateParameters.Add("ID_BUILD", GridViewBuildings.SelectedValue)
        SqlDataSourceFlats.UpdateParameters.Add("OPER_GUID", FormsAuthentication.Decrypt(Request.Cookies("AppBillingCookie").Value).UserData)
        SqlDataSourceFlats.UpdateParameters.Add("APP_GUID", ConfigurationManager.AppSettings.Item("app_guid").ToString)
        SqlDataSourceFlats.UpdateParameters.Add("HOST", HttpContext.Current.Request.UserHostAddress)

    End Sub

    Protected Sub GridViewFlats_RowUpdated(sender As Object, e As GridViewUpdatedEventArgs) Handles GridViewFlats.RowUpdated
        If e.Exception IsNot Nothing Then
            ErrMsg(e.Exception.Message)
            e.ExceptionHandled = True

            UpdatePanelError.Update()

        End If
    End Sub

    Protected Sub GridViewFlats_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridViewFlats.PageIndexChanging
        GridViewFlats.PageIndex = e.NewPageIndex
        GridViewFlats.DataBind()
        upModal.Update()
    End Sub

    Protected Sub GridViewFlats_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles GridViewFlats.RowDeleting
        SqlDataSourceFlats.DeleteCommand = "execute APP_Flat_Edit 3,@id_flats,NULL,0,0,NULL,NULL,@ID_BUILD,@OPER_GUID,@APP_GUID,@HOST"

        SqlDataSourceFlats.DeleteParameters.Add("ID_BUILD", GridViewBuildings.SelectedValue)
        SqlDataSourceFlats.DeleteParameters.Add("OPER_GUID", FormsAuthentication.Decrypt(Request.Cookies("AppBillingCookie").Value).UserData)
        SqlDataSourceFlats.DeleteParameters.Add("APP_GUID", ConfigurationManager.AppSettings.Item("app_guid").ToString)
        SqlDataSourceFlats.DeleteParameters.Add("HOST", HttpContext.Current.Request.UserHostAddress)
    End Sub

    Protected Sub GridViewFlats_RowDeleted(sender As Object, e As GridViewDeletedEventArgs) Handles GridViewFlats.RowDeleted
        If e.Exception IsNot Nothing Then
            ErrMsg(e.Exception.Message)
            e.ExceptionHandled = True

            UpdatePanelError.Update()
        End If
    End Sub


    Private Function ErrMsg(msg As String)
        LabelChangeError.Text = msg
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modal_error", "$('#modal_error').modal({backdrop: 'static',keyboard: false });", True)
        UpdatePanelError.Update()
        Return Nothing
    End Function
End Class