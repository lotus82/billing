Imports Microsoft.Security.Application
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Helpers
Imports System.Security.Cryptography

Public Class Login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (System.Threading.Thread.CurrentPrincipal.Identity.IsAuthenticated) = True Then
            Response.Redirect("~/ExitForm.aspx")
        End If
    End Sub

    Protected Sub ButtonLogin_Click(sender As Object, e As EventArgs) Handles ButtonLogin.Click
        Dim con As String
        con = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
        Dim ticket As FormsAuthenticationTicket
        Dim uGuid As Guid, strEncrypted As String, role As Integer
        LabelInfo.Text = ""
        If String.IsNullOrEmpty(Encoder.HtmlEncode(Request.Form("Login"))) Then
            LabelInfo.Text = "Введите имя пользователя!"
            LabelInfo.ForeColor = Drawing.Color.Red
            Exit Sub
        ElseIf String.IsNullOrEmpty(Encoder.HtmlEncode(Request.Form("PSW"))) Then
            LabelInfo.Text = "Введите пароль!"
            LabelInfo.ForeColor = Drawing.Color.Red
            Exit Sub
        End If
        '**************************************************************************
        Try


            Using MyConnection = New SqlConnection(con)
                Dim cm As New SqlCommand("Oper_login", MyConnection)
                Dim dr As SqlDataReader
                cm.CommandType = CommandType.StoredProcedure
                Dim prmName = New SqlParameter("@Oper_name", SqlDbType.NVarChar, 50)
                prmName.Value = Encoder.HtmlEncode(Request.Form("Login"))
                cm.Parameters.Add(prmName)
                Dim prmPass = New SqlParameter("@Oper_pass", SqlDbType.NVarChar, 50)
                prmPass.Value = getMD5Hash(Encoder.HtmlEncode(Request.Form("PSW")))
                cm.Parameters.Add(prmPass)
                Dim prmIP = New SqlParameter("@Oper_ip", SqlDbType.NVarChar, 50)
                prmIP.Value = HttpContext.Current.Request.UserHostAddress
                cm.Parameters.Add(prmIP)
                Dim prmSS = New SqlParameter("@app_guid", SqlDbType.NVarChar, 50)
                prmSS.Value = ConfigurationManager.AppSettings.Item("app_guid").ToString
                cm.Parameters.Add(prmSS)
                MyConnection.Open()
                dr = cm.ExecuteReader


                If dr.HasRows = True Then

                    While dr.Read()

                        Session.Add("user_session", Cr2015.CDCrypt.Encrypt(dr(0).ToString, "*****"))
                        uGuid = dr(2)
                        role = dr(3)

                    End While



                    '****************************************
                    Dim isPersistent As Boolean = False
                    ticket = New FormsAuthenticationTicket(1, "AppBilling", DateTime.Now, DateTime.Now.AddMinutes(90), False, uGuid.ToString, FormsAuthentication.FormsCookiePath)
                    strEncrypted = FormsAuthentication.Encrypt(ticket)
                    ' Сохраняем cookie-файл
                    Response.Cookies.Add(New HttpCookie("AppBillingCookie", strEncrypted))

                    Response.Cookies.Add(New HttpCookie("AppBillingCookie2", Cr2015.CDCrypt.Encrypt(role.ToString, "*****")))


                    Response.Redirect("~/APP.aspx")
                End If

                dr.Close()
            End Using

        Catch ex As Exception
            LabelInfo.Text = ex.Message
            LabelInfo.ForeColor = Drawing.Color.Red
        End Try



    End Sub

    Function getMD5Hash(ByVal strToHash As String) As String
        Dim md5Obj As New MD5CryptoServiceProvider
        Dim ByteHash() As Byte = System.Text.Encoding.Default.GetBytes(strToHash)
        ByteHash = md5Obj.ComputeHash(ByteHash)
        Dim strResult As String = ""
        For Each b As Byte In ByteHash
            strResult += b.ToString("x2")
        Next
        Return strResult
    End Function


End Class