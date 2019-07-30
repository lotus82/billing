<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="App_Billing.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=yes"/>
    <meta name="description" content=""/>
    <meta name="author" content=""/>
    <link rel="stylesheet" type="text/css" href="Content/bootstrap.min.css" />
    <link href="Styles/signin.css" rel="stylesheet"/>
      <link href="../Images/billing.ico" rel="shortcut icon" type="image/x-icon" />
    <title>Вход</title>
</head>
<body>
 <div class="container"/>
      <form class="form-signin" runat="server">
          
        <h3  style="text-align:center">Вход в систему расчетов ЖКХ</h3>
      
          <br />
      <label for="inputEmail" class="sr-only">Имя пользователя</label>
     <input type="text" id="inputEmail" class="form-control" placeholder="Имя пользователя"  name="Login"   autofocus="autofocus" />
                  
       
                  <label for="inputPassword" class="sr-only">Пароль</label>
                 <input type="password" id="inputPassword" class="form-control" name="PSW" placeholder="Пароль" />

     
     <br />
          <asp:Button ID="ButtonLogin"  CssClass="btn btn-lg btn-block" ForeColor="#ff6800" runat="server" Text="Войти" />
       
        
          <br />
           <asp:Label ID="LabelInfo" runat="server" Text="" Font-Size="16"></asp:Label>
               
      </form>
     <script src="Scripts/ie10-viewport-bug-workaround.js"></script>
</body>
</html>
