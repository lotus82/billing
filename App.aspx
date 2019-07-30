<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/App.Master" CodeBehind="App.aspx.vb" Inherits="App_Billing.App1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" style="border: 1px solid LightGray; background-color: #f8f6f6; border-radius: 3px; padding: 10px; width: 100%; margin-top: 10px;">
        <div class="alert-danger">
            <asp:Label ID="Label_danger" runat="server" Text=""></asp:Label>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <%-- -------------------------------------------------------Главный контейнер-------------------------------------------------------------------------------%>
                <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" Width="100%" ActiveTabIndex="1" AutoPostBack="True">
                    <%-- ---------------------------------------------------Вкладка "Поиск по адресу"-----------------------------------------------------------------------%>
                    <ajaxToolkit:TabPanel ID="TabPanelAddress" runat="server" HeaderText="Поиск по адресу" ToolTip="Поиск по адресу" Height="100">
                        <HeaderTemplate>
                            <p><span class="glyphicon glyphicon-th-list"></span>&nbsp;Поиск по адресу</p>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <asp:UpdatePanel ID="UpdatePanel_SEARCH_ADDRESS_MAIN" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="row" style="margin: 20px 20px 20px 20px">
                                        <div class="col-md-4">
                                            <label for="DropDownList_UL" style="font-size: 12px">Улица:</label>
                                            <asp:DropDownList ID="DropDownList_UL" runat="server" AutoPostBack="True" ForeColor="Black" Font-Size="12px" AppendDataBoundItems="True" ToolTip="Выбор улицы">
                                                <asp:ListItem Value="-Выбор улицы-" Text="-Выбор улицы-"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-3">
                                            <label for="DropDownList_Bldn" style="font-size: 12px">Дом:</label>
                                            <asp:DropDownList ID="DropDownList_Bldn" runat="server" AutoPostBack="True" ForeColor="Black" Font-Size="12px"></asp:DropDownList>
                                        </div>
                                        <div class="col-md-3">
                                            <label for="DropDownList_FLT" style="font-size: 12px">Квартира:</label>
                                            <asp:DropDownList ID="DropDownList_FLT" runat="server" AutoPostBack="True" ForeColor="Black" Font-Size="12px"></asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:ImageButton ID="ImageButtonAddOCC_ID" runat="server" ImageAlign="Left" ImageUrl="~/Images/Plus.png" Width="22px" ToolTip="Добавить лицевой счет" Visible="False" />
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <%-- ---------------------------------------------------Вкладка "Поиск по лицевому счету"---------------------------------------------------------------%>
                    <ajaxToolkit:TabPanel ID="TabPanelOcc_id" runat="server" HeaderText="Поиск по лицевому счету" ToolTip="Поиск по лицевому счету" Height="100">
                        <HeaderTemplate>
                            <p><span class="glyphicon glyphicon-align-justify"></span>&nbsp;Поиск по лицевому счету</p>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="row" style="margin: 20px 20px 20px 20px">
                                        <asp:Label ID="Label2" runat="server" Text="Введите номер лицевого счета, например 929102230"></asp:Label>
                                        <br />
                                        <asp:TextBox ID="TextBox1" runat="server" MaxLength="9" Width="215px"></asp:TextBox>
                                        <asp:Button ID="Button_LS" runat="server" Text="Найти" /><br />
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <%-- ---------------------------------------------------Вкладка "Поиск по фамилии"----------------------------------------------------------------------%>
                    <ajaxToolkit:TabPanel ID="TabPanelFIO" runat="server" HeaderText="Поиск по Ф.И.О." ToolTip="Поиск по Ф.И.О." Height="100">
                        <HeaderTemplate>
                            <p><span class="glyphicon glyphicon-user"></span>&nbsp;Поиск по Ф.И.О.</p>
                        </HeaderTemplate>
                        <ContentTemplate>
                            <div class="row" style="margin: 20px 20px 20px 20px">
                                <center><asp:Button ID="Button_Familia" runat="server"  Text="Найти" /></center>
                                <asp:Label ID="Label_search_familia" runat="server" Text=""></asp:Label>
                            </div>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                </ajaxToolkit:TabContainer>
                <br />
                <asp:UpdatePanel ID="UpdatePanel_LS" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Panel ID="Panel3" runat="server" BackColor="White">
                            <asp:GridView ID="GridView_LS" AutoGenerateColumns="False" runat="server" CellPadding="1" ShowHeaderWhenEmpty="True" DataKeyNames="ID"
                                Width="100%" AllowPaging="True" UseAccessibleHeader="False" Font-Size="8pt"
                                ForeColor="#666666" PageSize="20" EmptyDataText="Лицевой счет не найден">
                                <HeaderStyle BackColor="#EFEFEF" ForeColor="#FF6800" HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
                                <PagerStyle BackColor="#EFEFEF" CssClass="GridPager"></PagerStyle>
                                <RowStyle BorderColor="#FF6800" HorizontalAlign="Center"></RowStyle>
                                <Columns>
                                    <asp:ButtonField ButtonType="Image" ImageUrl="Images/Button.png" Text="Выбрать" CommandName="edit_click" ControlStyle-Width="18px" />
                                    <asp:BoundField HeaderText="Лицевой счет" DataField="ID" />
                                    <asp:BoundField HeaderText="Ответственный" DataField="OTVL" />
                                    <asp:BoundField HeaderText="Адрес" DataField="Addr" />
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <br />
                <asp:UpdatePanel ID="UpdatePanel_Detail" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="row" style="margin: 0px 0px 0px 0px">
                            <%-- ---------------------------------------Контейнер "Подробная информация"--------------------------------------------------------------------%>
                            <ajaxToolkit:TabContainer ID="TabContainer_Detail" runat="server" Width="100%" ActiveTabIndex="2" AutoPostBack="True">
                                <%-- ---------------------------------------Вкладка "Информация по Лицевому счету"----------------------------------------------------------%>
                                <ajaxToolkit:TabPanel ID="TabPanel_LS_info" runat="server" HeaderText="Лицевой счет" ToolTip="Лицевой счет">
                                    <HeaderTemplate>
                                        <p><span class="glyphicon glyphicon-align-justify"></span>&nbsp;Лицевой счет</p>
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                        
                                        <asp:UpdatePanel ID="UpdatePanel_Table_LS" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <h4><asp:Label ID="Label_LS_info_header" runat="server" Text="Данные о лицевом счете"></asp:Label></h4>
                                                <hr />
                                                <asp:Table ID="Table_LS" runat="server">
                                                    <asp:TableRow runat="server">
                                                        <asp:TableCell Width="220px" runat="server">
                                                                           Лицевой счет
                                                        </asp:TableCell>
                                                        <asp:TableCell Width="320px" runat="server">
                                                            <asp:TextBox ID="TextBox_Familia_Serch_1_ID" runat="server" ReadOnly="True" Width="300px" Enabled="False"></asp:TextBox>
                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FTB11" runat="server" TargetControlID="TextBox_Familia_Serch_1_ID" ValidChars="0123456789" BehaviorID="_content_FTB11" />
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                    <asp:TableRow runat="server">
                                                        <asp:TableCell Width="220px" runat="server">
                                                                            Номер договора
                                                        </asp:TableCell>
                                                        <asp:TableCell Width="320px" runat="server">
                                                            <asp:TextBox ID="TextBox_Dog_num" runat="server" Width="300px" AutoPostBack="true"></asp:TextBox>
                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FTB12" runat="server" TargetControlID="TextBox_Dog_num" ValidChars="0123456789абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИКЛМНОПРСТУФХЦЧШЩЭЮЯ-" BehaviorID="_content_FTB12" />
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                    <asp:TableRow runat="server">
                                                        <asp:TableCell Width="220px" runat="server">
                                                                            Тип Л/С
                                                        </asp:TableCell>
                                                        <asp:TableCell Width="320px" runat="server">
                                                            <asp:DropDownList ID="DropDownList_OCC_type" runat="server" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_OCC_type_SelectedIndexChanged"></asp:DropDownList>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                    <asp:TableRow runat="server">
                                                        <asp:TableCell Width="220px" runat="server">
                                                                            Статус Л/С
                                                        </asp:TableCell>
                                                        <asp:TableCell Width="320px" runat="server">
                                                            <asp:DropDownList ID="DropDownList_OCC_status" runat="server" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_OCC_status_SelectedIndexChanged"></asp:DropDownList>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                    <asp:TableRow runat="server">
                                                        <asp:TableCell Width="220px" runat="server">
                                                                            Свойтство Л/С
                                                        </asp:TableCell>
                                                        <asp:TableCell Width="320px" runat="server">
                                                            <asp:DropDownList ID="DropDownList_OCC_property" runat="server" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="DropDownList_OCC_property_SelectedIndexChanged"></asp:DropDownList>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                    <asp:TableRow runat="server">
                                                        <asp:TableCell Width="220px" runat="server">
                                                                            Площадь общая 
                                                        </asp:TableCell>
                                                        <asp:TableCell Width="320px" runat="server">
                                                            <asp:TextBox ID="TextBox_SQ" runat="server" Width="300px" AutoPostBack="True" TextMode="SingleLine" MaxLength="11"></asp:TextBox>
                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FTB13" runat="server" TargetControlID="TextBox_SQ" ValidChars="0123456789." BehaviorID="_content_FTB13" />
                                                        </asp:TableCell>
                                                        <asp:TableCell Width="350px">
                                                            <asp:Label ID="Label_SQ_info" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                    <asp:TableRow runat="server">
                                                        <asp:TableCell Width="220px" runat="server">
                                                                            Площадь жилая
                                                        </asp:TableCell>
                                                        <asp:TableCell Width="320px" runat="server">
                                                            <asp:TextBox ID="TextBox_SQ_live" runat="server" Width="300px" AutoPostBack="True"></asp:TextBox>
                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FTB14" runat="server" TargetControlID="TextBox_SQ_live" ValidChars="0123456789." BehaviorID="_content_FTB14" />
                                                        </asp:TableCell>
                                                        <asp:TableCell Width="350px">
                                                            <asp:Label ID="Label_SQ_live_info" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                    <asp:TableRow runat="server">
                                                        <asp:TableCell Width="220px" runat="server">
                                                                            Отапливаемая площадь
                                                        </asp:TableCell>
                                                        <asp:TableCell Width="320px" runat="server">
                                                            <asp:TextBox ID="TextBox_SQ_hot" runat="server" Width="300px" AutoPostBack="True"></asp:TextBox>
                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FTB15" runat="server" TargetControlID="TextBox_SQ_hot" ValidChars="0123456789." BehaviorID="_content_FTB15" />
                                                        </asp:TableCell>
                                                        <asp:TableCell Width="350px">
                                                            <asp:Label ID="Label_SQ_hot_info" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                    <asp:TableRow runat="server">
                                                        <asp:TableCell Width="220px" runat="server">
                                                                            Площадь МОП
                                                        </asp:TableCell>
                                                        <asp:TableCell Width="320px" runat="server">
                                                            <asp:TextBox ID="TextBox_SQ_MOP" runat="server" Width="300px" AutoPostBack="True"></asp:TextBox>
                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FTB16" runat="server" TargetControlID="TextBox_SQ_MOP" ValidChars="0123456789." BehaviorID="_content_FTB16" />
                                                        </asp:TableCell>
                                                        <asp:TableCell Width="350px">
                                                            <asp:Label ID="Label_SQ_MOP_info" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                    <asp:TableRow runat="server">
                                                        <asp:TableCell Width="220px" runat="server">
                                                                            Реально проживающие
                                                        </asp:TableCell>
                                                        <asp:TableCell Width="320px" runat="server">
                                                            <asp:TextBox ID="TextBox_Real_live" runat="server" Width="300px" Enabled="False"></asp:TextBox>
                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FTB17" runat="server" TargetControlID="TextBox_Real_live" ValidChars="0123456789" BehaviorID="_content_FTB17" />
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                    <asp:TableRow runat="server">
                                                        <asp:TableCell Width="220px" runat="server">
                                                                            Email
                                                        </asp:TableCell>
                                                        <asp:TableCell Width="320px" runat="server">
                                                            <asp:TextBox ID="TextBox_Email" runat="server" Width="300px" AutoPostBack="True"></asp:TextBox>
                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FTB18" runat="server" TargetControlID="TextBox_Email" ValidChars="0123456789@abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ.-_абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИКЛМНОПРСТУФХЦЧШЩЭЮЯ" BehaviorID="_content_FTB18" />
                                                        </asp:TableCell>
                                                        <asp:TableCell Width="350px">
                                                            <asp:Label ID="Label_Email_info" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                    <asp:TableRow runat="server">
                                                        <asp:TableCell Width="220px" runat="server">
                                                                            Телефон
                                                        </asp:TableCell>
                                                        <asp:TableCell Width="320px" runat="server">
                                                            <asp:TextBox ID="TextBox_Phone" runat="server" Width="300px" AutoPostBack="True" MaxLength="11"></asp:TextBox>
                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FTB19" runat="server" TargetControlID="TextBox_Phone" ValidChars="0123456789" BehaviorID="_content_FTB19" />
                                                        </asp:TableCell>
                                                        <asp:TableCell Width="350px">
                                                            <asp:Label ID="Label_Phone_info" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                    <asp:TableRow runat="server">
                                                        <asp:TableCell Width="220px" runat="server">
                                                                            Телефон (дополнительно)
                                                        </asp:TableCell>
                                                        <asp:TableCell Width="320px" runat="server">
                                                            <asp:TextBox ID="TextBox_Phone2" runat="server" Width="300px" AutoPostBack="True" MaxLength="11"></asp:TextBox>
                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FTB20" runat="server" TargetControlID="TextBox_Phone2" ValidChars="0123456789" BehaviorID="_content_FTB20" />
                                                        </asp:TableCell>
                                                        <asp:TableCell Width="350px">
                                                            <asp:Label ID="Label_Phone2_info" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                    <asp:TableRow runat="server">
                                                        <asp:TableCell Width="220px" runat="server">
                                                                            Кадастр
                                                        </asp:TableCell>
                                                        <asp:TableCell Width="320px" runat="server">
                                                            <asp:TextBox ID="TextBox_Kadastr" runat="server" Width="300px" AutoPostBack="True"></asp:TextBox>
                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FTB21" runat="server" TargetControlID="TextBox_Kadastr" ValidChars="0123456789" BehaviorID="_content_FTB21" />
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                    <asp:TableRow runat="server">
                                                        <asp:TableCell Width="220px" runat="server">
                                                                            ГИС
                                                        </asp:TableCell>
                                                        <asp:TableCell Width="320px" runat="server">
                                                            <asp:TextBox ID="TextBox_GIS" runat="server" Width="300px" AutoPostBack="True"></asp:TextBox>
                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FTB22" runat="server" TargetControlID="TextBox_GIS" ValidChars="0123456789" BehaviorID="_content_FTB22" />
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                    <asp:TableRow runat="server">
                                                        <asp:TableCell Width="220px" runat="server">
                                                                            Код
                                                        </asp:TableCell>
                                                        <asp:TableCell Width="320px" runat="server">
                                                            <asp:TextBox ID="TextBox_Code" runat="server" Width="300px" AutoPostBack="True"></asp:TextBox>
                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FTB23" runat="server" TargetControlID="TextBox_Code" ValidChars="0123456789" BehaviorID="_content_FTB23" />
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                    <asp:TableRow runat="server">
                                                        <asp:TableCell Width="220px" runat="server">
                                                                            Дата создания
                                                        </asp:TableCell>
                                                        <asp:TableCell Width="320px" runat="server">
                                                            <asp:TextBox ID="TextBox_Start_date" runat="server" Width="300px" Enabled="false" AutoPostBack="True" MaxLength="10"></asp:TextBox>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                    <asp:TableRow runat="server">
                                                        <asp:TableCell Width="220px" runat="server">
                                                                            Дата окончания
                                                        </asp:TableCell>
                                                        <asp:TableCell Width="320px" runat="server">
                                                            <asp:TextBox ID="TextBox_End_date" runat="server" Width="300px" Enabled="false" AutoPostBack="True" MaxLength="10"></asp:TextBox>
                                                        </asp:TableCell>
                                                        <asp:TableCell Width="350px">
                                                            <asp:Label ID="Label_End_date_info" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                    <asp:TableRow runat="server">
                                                        <asp:TableCell Width="220px" runat="server">
                                                                            Закрытие
                                                        </asp:TableCell>
                                                        <asp:TableCell Width="320px" runat="server">
                                                            <asp:DropDownList ID="DropDownList_Is_closed" runat="server" Width="300px" AutoPostBack="True"></asp:DropDownList>
                                                        </asp:TableCell>
                                                        <asp:TableCell Width="350px">
                                                            <asp:Label ID="Label1_Is_closed_info" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                </asp:Table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        

                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>

                                <%-- ---------------------------------------Вкладка "Информация о людях"--------------------------------------------------------------------%>
                                <ajaxToolkit:TabPanel ID="TabPanel_User_info" runat="server" HeaderText="Люди" ToolTip="Люди">
                                    <HeaderTemplate>
                                        <p><span class="glyphicon glyphicon-user"></span>&nbsp;Люди</p>
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                        <asp:UpdatePanel ID="UpdatePanel_User_info" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <h4><asp:Label ID="Label_User_info_header" runat="server" Text="Данные о людях"></asp:Label></h4>
                                                <hr />
                                                <asp:Label ID="Label_User_Update_Error" runat="server" Text="" ForeColor="Red" Font-Size="Large"></asp:Label>
                                                <asp:Panel ID="Panel_User_info" runat="server" ScrollBars="Auto">
                                                    <asp:GridView ID="GridView_User_info" AutoGenerateColumns="False" runat="server" CellPadding="1" ShowHeaderWhenEmpty="True" DataKeyNames="GUID_user"
                                                        Width="100%" AllowPaging="True" UseAccessibleHeader="False"
                                                        ForeColor="#666666" PageSize="20" EmptyDataText="Люди по данному Л/С не найдены" Font-Size="8pt">
                                                        <HeaderStyle BackColor="#EFEFEF" ForeColor="#FF6800" HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
                                                        <PagerStyle BackColor="#EFEFEF" CssClass="GridPager"></PagerStyle>
                                                        <RowStyle BorderColor="#FF6800" HorizontalAlign="Center"></RowStyle>
                                                        <Columns>
                                                            <asp:ButtonField ButtonType="Image" ImageUrl="Images/Doc.png" Text="Документы"  CommandName="edit_click" ControlStyle-Width="18" />
                                                            <asp:BoundField HeaderText="GUID_user" DataField="GUID_user" ReadOnly="true" Visible="false" />
                                                            <asp:TemplateField HeaderText="Фамилия">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label_User_Surname_Item" runat="server" Text='<%# Eval("Surname")%>' ToolTip="Фамилия" Width="130px"  ReadOnly="true"></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox_Familia_Serch_2_User_Surname_Edit" runat="server" Text='<%# Eval("Surname")%>' ToolTip="Фамилия" Width="130" ReadOnly="false" autofocus="true" placeholder="Фамилия"></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="TextBox_Familia_Serch_2_User_Surname_Edit_FTB" runat="server" FilterType="Custom" TargetControlID="TextBox_Familia_Serch_2_User_Surname_Edit" ValidChars="абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИКЛМНОПРСТУФХЦЧШЩЭЮЯ0123456789" />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Имя">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label_User_Name_Item" runat="server" Text='<%# Eval("Name")%>' ToolTip="Имя" Width="130px"  ReadOnly="true"></asp:Label>
                                                                    <%--<asp:TextBox ID="TextBox_Familia_Serch_2_User_Name_Item" runat="server" Text='<%# Eval("Name")%>' ToolTip="Имя" Width="130" ReadOnly="true"></asp:TextBox>--%>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox_Familia_Serch_2_User_Name_Edit" runat="server" Text='<%# Eval("Name")%>' ToolTip="Имя" Width="130" ReadOnly="false"></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="TextBox_Familia_Serch_2_User_Name_Edit_FTB" runat="server" FilterType="Custom" TargetControlID="TextBox_Familia_Serch_2_User_Name_Edit" ValidChars="абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИКЛМНОПРСТУФХЦЧШЩЭЮЯ0123456789" />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Отчество">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label_User_Patronymic_Item" runat="server" Text='<%# Eval("Patronymic")%>' ToolTip="Отчество" Width="130px"  ReadOnly="true"></asp:Label>
                                                                    <%--<asp:TextBox ID="TextBox_Familia_Serch_2_User_Patronymic_Item" runat="server" Text='<%# Eval("Patronymic")%>' ToolTip="Отчество" Width="130" ReadOnly="true"></asp:TextBox>--%>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox_Familia_Serch_2_User_Patronymic_Edit" runat="server" Text='<%# Eval("Patronymic")%>' ToolTip="Отчество" Width="130" ReadOnly="false"></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="TextBox_Familia_Serch_2_User_Patronymic_Edit_FTB" runat="server" FilterType="Custom" TargetControlID="TextBox_Familia_Serch_2_User_Patronymic_Edit" ValidChars="абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИКЛМНОПРСТУФХЦЧШЩЭЮЯ0123456789" />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Дата рождения" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label_Familia_Serch_2_User_Datebirth_Item" runat="server" Text='<%# Eval("Date_birth", "{0:dd.MM.yyyy}")%>' ToolTip="Дата рождения" Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox_Familia_Serch_2_User_Datebirth_Edit" runat="server" Text='<%# Eval("Date_birth", "{0:yyyy-MM-dd}")%>' TextMode="Date" BackColor="#FFFFCC" ToolTip="Дата рождения" ReadOnly="false" Width="115px"></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="TextBox_Familia_Serch_2_User_Datebirth_Edit_FTB" runat="server" FilterType="Numbers, Custom" TargetControlID="TextBox_Familia_Serch_2_User_Datebirth_Edit" ValidChars=".-" />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Пол">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label_User_Sex_Item" runat="server" Text='<%# Eval("Sex")%>' ToolTip="Пол" Width="30px"></asp:Label>
                                                                    <%--<asp:DropDownList ID="DropDownList_User_Sex_Item" runat="server" Width="80px" Enabled="false"></asp:DropDownList>--%>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:DropDownList ID="DropDownList_User_Sex_Edit" runat="server" Width="40px"></asp:DropDownList>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Отношения" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label_User_Relation_Item" runat="server" Text='<%# Eval("WHO")%>' ToolTip="Отношения" Width="130px"></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:DropDownList ID="DropDownList_User_Relation_Edit" runat="server" Width="120px"></asp:DropDownList>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Регистрация" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label_User_Reg_Item" runat="server" Text='<%# Eval("reg_type_name")%>' ToolTip="Регистрации" Width="130px"></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:DropDownList ID="DropDownList_User_Reg_Edit" runat="server" Width="120px"></asp:DropDownList>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Дата рег." ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label_Familia_Serch_2_User_DateReg_Item" runat="server" Text='<%# Eval("Date_registration", "{0:dd.MM.yyyy}")%>' ToolTip="Дата регистрации" Width="60px"></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox_Familia_Serch_2_User_DateReg_Edit" runat="server" Text='<%# Eval("Date_registration", "{0:yyyy-MM-dd}")%>' TextMode="Date" ReadOnly="True" BackColor="#FFFFCC" ToolTip="Дата регистрации" Width="115px"></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="TextBox_Familia_Serch_2_User_DateReg_Edit_FTB" runat="server" FilterType="Numbers, Custom" TargetControlID="TextBox_Familia_Serch_2_User_DateReg_Edit" ValidChars=".-" />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Окончание рег." ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label_User_EndReg_Item" runat="server" Text='<%# Eval("Date_registration_end", "{0:dd.MM.yyyy}")%>' ToolTip="Окончание регистрации" Width="90px"></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox_User_EndReg_Edit" runat="server" Text='<%# Eval("Date_registration_end", "{0:yyyy-MM-dd}")%>' TextMode="Date"  BackColor="#FFFFCC" ToolTip="Окончание регистрации" Width="115px"></asp:TextBox>
                                                                    <%--<ajaxToolkit:FilteredTextBoxExtender ID="TextBox_User_EndReg_Edit_FTB" runat="server" FilterType="Numbers, Custom" TargetControlID="TextBox_User_EndReg_Edit" ValidChars=".-" />--%>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ShowHeader="False" ItemStyle-HorizontalAlign="Left" ControlStyle-Width="18" ItemStyle-Wrap="false">
                                                                <EditItemTemplate>
                                                                    <asp:ImageButton ID="ImageButton_Familia_Serch_2_User_Update" runat="server" CausesValidation="True" CommandName="Update" ImageUrl="~/images/save.png" Text="Сохранить" ToolTip="Сохранить" Width="18px" />
                                                                    <asp:ImageButton ID="ImageButton_Familia_Serch_2_User_Cancel" runat="server" CausesValidation="True" CommandName="Cancel" ImageUrl="~/images/cancel.png" Text="Отменить" ToolTip="Отменить" Width="18px" />
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="ImageButton_User_Edit" runat="server" CausesValidation="False" CommandName="Edit" ImageUrl="~/images/edit.png" Text="Изменить" ToolTip="Изменить" Width="18px" />
                                                                    <asp:ImageButton ID="ImageButton_User_Unreg" runat="server" CausesValidation="True" CommandName="Delete" ImageUrl="~/Images/delete.png" Text="Выписать" ToolTip="Выписать" Width="18px" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>
                                                <br />
                                                <asp:Button ID="Button_Reg_User" CssClass="Btn" runat="server" Text="Регистрация" />
                                                <hr />
                                                <h4>Документы</h4>
                                                <hr />
                                                <asp:Panel ID="Panel_Doc" runat="server" ScrollBars="Auto">
                                                    <asp:GridView ID="GridView_Doc" AutoGenerateColumns="False" runat="server"  CellPadding="1" ShowHeaderWhenEmpty="True" DataKeyNames="ID_user_doc"
                                                        EnablePersistedSelection="True" Width="100%" AllowPaging="True" UseAccessibleHeader="False" Font-Size="8pt"
                                                        ForeColor="#666666" PageSize="20" EmptyDataText="Документы не найдены">
                                                        <HeaderStyle BackColor="#EFEFEF" ForeColor="#FF6800" HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
                                                        <PagerStyle BackColor="#EFEFEF" CssClass="GridPager"></PagerStyle>
                                                        <RowStyle BorderColor="#FF6800" HorizontalAlign="Center"></RowStyle>
                                                        <Columns>
                                                            
                                                            <asp:TemplateField HeaderText="Документ" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label_Doc_Type_Item" runat="server" Text='<%# Eval("Type")%>' ToolTip="Документ" Width="90px"></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:DropDownList ID="DropDownList_Doc_Type_Edit" runat="server">
                                                                         
                                                                    </asp:DropDownList>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            
                                                            <asp:TemplateField HeaderText="Серия" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label_Series_Item" runat="server" Text='<%# Eval("Series")%>' ToolTip="Серия" Width="90px"></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox_Series_Edit" runat="server" Text='<%# Eval("Series")%>' BackColor="#FFFFCC" ToolTip="Серия" ReadOnly="false" Width="135px" MaxLength="4"></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="TextBox_Series_Edit_FTB" runat="server" FilterType="Numbers" TargetControlID="TextBox_Series_Edit"  />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Номер" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label_Number_Item" runat="server" Text='<%# Eval("Number")%>' ToolTip="Номер" Width="90px"></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox_Number_Edit" runat="server" Text='<%# Eval("Number")%>' BackColor="#FFFFCC" ToolTip="Номер" ReadOnly="false" Width="135px" MaxLength="6"></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="TextBox_Number_Edit_FTB" runat="server" FilterType="Numbers" TargetControlID="TextBox_Number_Edit"  />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Дата документа" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label_Doc_Date_Item" runat="server" Text='<%# Eval("Date_Doc", "{0:dd.MM.yyyy}")%>' ToolTip="Дата документа" Width="90px"></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox_Doc_Date_Edit" runat="server" Text='<%# Eval("Date_Doc", "{0:yyyy-MM-dd}")%>' TextMode="Date" BackColor="#FFFFCC" ToolTip="Дата документа" ReadOnly="false" Width="135px"></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="TextBox_Doc_Date_Edit_FTB" runat="server" FilterType="Numbers, Custom" TargetControlID="TextBox_Doc_Date_Edit" ValidChars=".-" />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Организация" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label_Organization_Item" runat="server" Text='<%# Eval("Organization")%>' ToolTip="Организация" Width="500px"></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox_Organization_Edit" runat="server" Text='<%# Eval("Organization")%>' BackColor="#FFFFCC" ToolTip="Организация" ReadOnly="false" Width="500px" MaxLength="100" ></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="TextBox_Organization_Edit_FTB" runat="server" FilterType="Numbers,Custom" TargetControlID="TextBox_Organization_Edit" ValidChars="АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя ,.-"  />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            
                                                            <asp:TemplateField HeaderText="Код подразделения" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label_Code_Item" runat="server" Text='<%# Eval("Code")%>' ToolTip="Код подразделения" Width="90px"></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox_Code_Edit" runat="server" Text='<%# Eval("Code")%>' BackColor="#FFFFCC" ToolTip="Код подразделения" ReadOnly="false" Width="135px" MaxLength="7"></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="TextBox_Code_Edit_FTB" runat="server" FilterType="Custom,Numbers" TargetControlID="TextBox_Code_Edit" ValidChars="-"  />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField ShowHeader="False" ItemStyle-HorizontalAlign="Left" ControlStyle-Width="18px" ItemStyle-Wrap="false">
                                                                <EditItemTemplate>
                                                                    <asp:ImageButton ID="ImageButton_Doc_Update" runat="server" CausesValidation="True" CommandName="Update" ImageUrl="~/images/save.png" Text="Сохранить" ToolTip="Сохранить" Width="18px" />
                                                                    <asp:ImageButton ID="ImageButton_Doc_Cancel" runat="server" CausesValidation="True" CommandName="Cancel" ImageUrl="~/images/cancel.png" Text="Отменить" ToolTip="Отменить" Width="18px" />
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="ImageButton_Doc_Edit" runat="server" CausesValidation="False" CommandName="Edit" ImageUrl="~/images/edit.png" Text="Изменить" ToolTip="Изменить" Width="18px" />
                                                                    <asp:ImageButton ID="ImageButton_Doc_Delete" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/images/delete.png" Text="Удалить" ToolTip="Удалить" Width="18px" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>
                                                <br />
                                                <asp:Button ID="Button_Doc_Add" CssClass="Btn" runat="server" Text="Добавить" Visible="false" OnClick="Button_Doc_Add_Click" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>

                                <%-- ---------------------------------------Вкладка "Приборы"-------------------------------------------------------------------------------%>
                                <ajaxToolkit:TabPanel ID="TabPanel_Counters_info" runat="server" HeaderText="Приборы" ToolTip="Приборы">
                                    <HeaderTemplate>
                                        <p><span class="glyphicon glyphicon-dashboard"></span>&nbsp;Приборы учета</p>
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                        <asp:UpdatePanel ID="UpdatePanel_Counters" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <h4><asp:Label ID="Label_Counters_Header" runat="server" Text=""></asp:Label></h4>
                                                <hr />
                                                <asp:Label ID="Label_Counters_Error" runat="server" Text="" ForeColor="Red" Font-Size="Large"></asp:Label>
                                                <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
                                                    <asp:GridView ID="GridView_Counters" AutoGenerateColumns="false" runat="server" CellPadding="1" ShowHeaderWhenEmpty="True" DataKeyNames="COUNTER_ID"
                                                       EnablePersistedSelection="True" Width="100%" AllowPaging="True" UseAccessibleHeader="False" Font-Size="8pt"
                                                        ForeColor="#666666" PageSize="20" EmptyDataText="Приборы учета не найдены" >
                                                        <HeaderStyle BackColor="#EFEFEF" ForeColor="#FF6800" HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
                                                        <PagerStyle BackColor="#EFEFEF" CssClass="GridPager"></PagerStyle>
                                                        <RowStyle BorderColor="#FF6800" HorizontalAlign="Center"></RowStyle>
                                                        <Columns>
                                                            <asp:BoundField HeaderText="COUNTER_ID" DataField="COUNTER_ID" ReadOnly="true" Visible="false" />
                                                            <asp:TemplateField HeaderText="Услуга">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label_Counters_Service_Item" runat="server" Text='<%# Eval("Service_ID")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox_Counters_Service_Edit" runat="server" Text='<%# Eval("Service_ID")%>' ToolTip="Услуга" Width="80" ReadOnly="true" autofocus="true" placeholder="Услуга"></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="TextBox_Counters_Service_Edit_FTB" runat="server" FilterType="Custom" TargetControlID="TextBox_Counters_Service_Edit" ValidChars="абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИКЛМНОПРСТУФХЦЧШЩЭЮЯ" />
                                                                    <%--<asp:DropDownList ID="DropDownList_Counters_Service_Edit" runat="server" Width="80px"></asp:DropDownList>--%>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Серийный номер">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label_Counters_Serial_Num_Item" runat="server" Text='<%# Eval("SERIAL_NUM")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox_Counters_Serial_Num_Edit" runat="server" Text='<%# Eval("SERIAL_NUM")%>' ToolTip="Серийный номер" Width="80" ReadOnly="false" autofocus="true" placeholder="Серийный номер"></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="TextBox_Counters_Serial_Num_Edit_FTB" runat="server" FilterType="Custom" TargetControlID="TextBox_Counters_Serial_Num_Edit" ValidChars="-/.,abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZабвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИКЛМНОПРСТУФХЦЧШЩЭЮЯ0123456789" />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Максимальное знач.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label_Counters_Max_Value_Item" runat="server" Text='<%# Eval("MAX_VALUE")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox_Counters_Max_Value_Edit" runat="server" Text='<%# Eval("MAX_VALUE")%>' ToolTip="Макс. знач." Width="80" ReadOnly="true" autofocus="true" placeholder="Максимальное значение"></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="TextBox_Counters_Max_Value_Edit_FTB" runat="server" FilterType="Custom" TargetControlID="TextBox_Counters_Max_Value_Edit" ValidChars="0123456789." />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Последние показания">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label_Counters_Last_Value_Item" runat="server" Text='<%# Eval("LAST_VALUE")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox_Counters_Last_Value_Edit" runat="server" Text='<%# Eval("LAST_VALUE")%>' ToolTip="Последние показания" Width="80" ReadOnly="false" autofocus="true" placeholder="Последние показания"></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="TextBox_Counters_Last_Value_Edit_FTB" runat="server" FilterType="Custom" TargetControlID="TextBox_Counters_Last_Value_Edit" ValidChars="0123456789." />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Текущие показания">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label_Counters_Cur_Value_Item" runat="server" Text='<%# Eval("CUR_VALUE")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox_Counters_Cur_Value_Edit" runat="server" Text='<%# Eval("CUR_VALUE")%>' ToolTip="Текущие показания" Width="80" ReadOnly="false" autofocus="true" placeholder="Текущие показания"></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="TextBox_Counters_Cur_Value_Edit_FTB" runat="server" FilterType="Custom" TargetControlID="TextBox_Counters_Cur_Value_Edit" ValidChars="0123456789." />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Текущие единицы">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label_Counters_Cur_Units_Item" runat="server" Text='<%# Eval("CUR_UNITS")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox_Counters_Cur_Units_Edit" runat="server" Text='<%# Eval("CUR_UNITS")%>' ToolTip="Текущие единицы" Width="80" ReadOnly="false" autofocus="true" placeholder="Текущие единицы"></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="TextBox_Counters_Cur_Units_Edit_FTB" runat="server" FilterType="Custom" TargetControlID="TextBox_Counters_Cur_Units_Edit" ValidChars="0123456789." />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Посл. дата показаний">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label_Counters_Last_Date_Item" runat="server" Text='<%# Eval("LAST_DATE", "{0:dd.MM.yyyy}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox_Counters_Last_Date_Edit" runat="server" Text='<%# Eval("LAST_DATE", "{0:yyyy-MM-dd}")%>' TextMode="Date" ToolTip="Последняя дата показаний" Width="120" ReadOnly="false" autofocus="true" placeholder="Последняя дата показаний"></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="TextBox_Counters_Last_Date_Edit_FTB" runat="server" FilterType="Custom" TargetControlID="TextBox_Counters_Last_Date_Edit" ValidChars="0123456789-:." />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Коэффициент">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label_Counters_Coeff_Item" runat="server" Text='<%# Eval("COEFF")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox_Counters_Coeff_Edit" runat="server" Text='<%# Eval("COEFF")%>' ToolTip="Коэффициент" Width="80" ReadOnly="true" autofocus="true" placeholder="Коэффициент"></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="TextBox_Counters_Coeff_Edit_FTB" runat="server" FilterType="Custom" TargetControlID="TextBox_Counters_Coeff_Edit" ValidChars="0123456789." />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Норма">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label_Counters_Norm_Item" runat="server" Text='<%# Eval("NORM")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox_Counters_Norm_Edit" runat="server" Text='<%# Eval("NORM")%>' ToolTip="Норма" Width="80" ReadOnly="false" autofocus="true" placeholder="Норма"></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="TextBox_Counters_Norm_Edit_FTB" runat="server" FilterType="Custom" TargetControlID="TextBox_Counters_Norm_Edit" ValidChars="0123456789." />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Люди">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label_Calc_People" runat="server" Text='<%# Eval("CALC_PEOPLE")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox_Calc_People_Edit" runat="server" Text='<%# Eval("CALC_PEOPLE")%>' ToolTip="Люди" Width="40" ReadOnly="false" autofocus="true" placeholder="Люди"></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="TextBox_Calc_People_Edit_FTB" runat="server" FilterType="Numbers" TargetControlID="TextBox_Calc_People_Edit" />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Заведение прибора">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label_Last_Date_Item" runat="server" Text='<%# Eval("DATE_CNTR", "{0:dd.MM.yyyy}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox_Date_Cntr_Edit" runat="server" Text='<%# Eval("DATE_CNTR", "{0:yyyy-MM-dd}")%>' TextMode="Date" ToolTip="Заведение прибора" Width="120" ReadOnly="false" autofocus="true" placeholder="Заведение прибора"></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="TextBox_Date_Cntr_Edit_FTB" runat="server" FilterType="Custom" TargetControlID="TextBox_Date_Cntr_Edit" ValidChars="0123456789-:." />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Дата установки">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label_Installation_Date_Item" runat="server" Text='<%# Eval("Installation_date", "{0:dd.MM.yyyy}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox_Installation_Date_Edit" runat="server" Text='<%# Eval("Installation_date", "{0:yyyy-MM-dd}")%>' TextMode="Date" ToolTip="Дата установки" Width="120" ReadOnly="false" autofocus="true" placeholder="Дата установки"></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="TextBox_Installation_Date_Edit_FTB" runat="server" FilterType="Custom" TargetControlID="TextBox_Installation_Date_Edit" ValidChars="0123456789-:." />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Ввод в эксплуатацию">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label_Date_Of_Commissioning_Item" runat="server" Text='<%# Eval("Date_of_commissioning", "{0:dd.MM.yyyy}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox_Date_Of_Commissioning_Edit" runat="server" Text='<%# Eval("Date_of_commissioning", "{0:yyyy-MM-dd}")%>' TextMode="Date" ToolTip="Ввод в эксплуатацию" Width="120" ReadOnly="false" autofocus="true" placeholder="Ввод в эксплуатацию"></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="TextBox_Date_Of_Commissioning_Edit_FTB" runat="server" FilterType="Custom" TargetControlID="TextBox_Date_Of_Commissioning_Edit" ValidChars="0123456789-:." />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Дата пломбировки">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label_Date_Plant_Mark_Item" runat="server" Text='<%# Eval("Date_plant_mark", "{0:dd.MM.yyyy}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox_Date_Plant_Mark_Edit" runat="server" Text='<%# Eval("Date_plant_mark", "{0:yyyy-MM-dd}")%>' TextMode="Date" ToolTip="Дата пломбировки" Width="120" ReadOnly="false" autofocus="true" placeholder="Дата пломбировки"></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="TextBox_Date_Plant_Mark_Edit_FTB" runat="server" FilterType="Custom" TargetControlID="TextBox_Date_Plant_Mark_Edit" ValidChars="0123456789-:." />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Следующая поверка">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label_Date_Of_Last_Verification_Item" runat="server" Text='<%# Eval("Date_of_last_verification", "{0:dd.MM.yyyy}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox_Date_Of_Last_Verification_Edit" runat="server" Text='<%# Eval("Date_of_last_verification", "{0:yyyy-MM-dd}")%>' TextMode="Date" ToolTip="Следующая поверка" Width="120" ReadOnly="true" autofocus="true" placeholder="Следующая поверка"></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="TextBox_Date_Of_Last_Verification_Edit_FTB" runat="server" FilterType="Custom" TargetControlID="TextBox_Date_Of_Last_Verification_Edit" ValidChars="0123456789-:." />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ShowHeader="False" ItemStyle-HorizontalAlign="Left" ControlStyle-Width="18" ItemStyle-Wrap="false">
                                                                <EditItemTemplate>
                                                                    <asp:ImageButton ID="ImageButton_Counters_Update" runat="server" CausesValidation="True" CommandName="Update" ImageUrl="~/images/save.png" Text="Сохранить" ToolTip="Сохранить" Width="18px" />
                                                                    <asp:ImageButton ID="ImageButton_Counters_Cancel" runat="server" CausesValidation="True" CommandName="Cancel" ImageUrl="~/images/cancel.png" Text="Отменить" ToolTip="Отменить" Width="18px" />
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="ImageButton_Counters_Edit" runat="server" CausesValidation="False" CommandName="Edit" ImageUrl="~/images/edit.png" Text="Изменить" ToolTip="Изменить" Width="18px" />
                                                                    <asp:ImageButton ID="ImageButton_Counters_Delete" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/images/delete.png" Text="Удалить" ToolTip="Удалить" Width="18px" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>
                                                <br />
                                                <asp:Button ID="Button_Counter_Add" CssClass="Btn" runat="server" Text="Добавить" OnClick="Button_Counter_Add_Click"/>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>

                                <%-- ---------------------------------------Вкладка "Начисления"----------------------------------------------------------------------------%>
                                <ajaxToolkit:TabPanel ID="TabPanel_Calc" runat="server" HeaderText="Начисления" ToolTip="Начисления">
                                    <HeaderTemplate>
                                        <p><span class="glyphicon glyphicon-stats"></span>&nbsp;Начисления</p>
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                        <asp:UpdatePanel ID="UpdatePanel_Calc" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <h4><asp:Label ID="Label_Calc_Header" runat="server" Text="Начисления"></asp:Label></h4>
                                                <hr />
                                                <asp:Label ID="Label_Calc_Error" runat="server" Text="" ForeColor="Red" Font-Size="Large"></asp:Label>
                                                <asp:Panel ID="Panel_Calc" runat="server" ScrollBars="Auto">
                                                   <asp:GridView ID="GridView_Calc" AutoGenerateColumns="False" runat="server" CellPadding="1" ShowHeaderWhenEmpty="True" DataKeyNames="SERVICE_ID"
                                                        Width="100%" AllowPaging="True" UseAccessibleHeader="False"
                                                        ForeColor="#666666" PageSize="20" EmptyDataText="Начисления не найдены" Font-Size="8pt">
                                                        <HeaderStyle BackColor="#EFEFEF" ForeColor="#FF6800" HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
                                                        <PagerStyle BackColor="#EFEFEF" CssClass="GridPager"></PagerStyle>
                                                        <RowStyle BorderColor="#FF6800" HorizontalAlign="Center"></RowStyle>
                                                        <Columns>
                                                            <asp:ButtonField ButtonType="Image" ImageUrl="Images/Doc.png" Text="Квитанции" CommandName="edit_click" ControlStyle-Width="18" />
                                                            <asp:BoundField HeaderText="Услуга" DataField="SERVICE_ID" ReadOnly="true" Visible="false" />
                                                            <asp:TemplateField HeaderText="Тариф">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label_Tarif_Item" runat="server" Text='<%# Eval("TAR_NAME")%>' ToolTip="Тариф" Width="160px"></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox_Tarif_Edit" runat="server" Text='<%# Eval("TAR_NAME")%>' ToolTip="Тариф" Width="160" ReadOnly="true" autofocus="true" placeholder="Тариф"></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="TextBox_Tarif_Edit_FTB" runat="server" FilterType="Numbers,Custom" TargetControlID="TextBox_Tarif_Edit" ValidChars="абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИКЛМНОПРСТУФХЦЧШЩЭЮЯ_-./|\,( ):$*#%;" />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Услуга">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label_Service_Item" runat="server" Text='<%# Eval("Service_Name")%>' ToolTip="Услуга" Width="190px"></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox_Service_Edit" runat="server" Text='<%# Eval("Service_Name")%>' ToolTip="Услуга" Width="190" ReadOnly="true" autofocus="true" placeholder="Тариф"></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="TextBox_Service_Edit_FTB" runat="server" FilterType="Numbers,Custom" TargetControlID="TextBox_Service_Edit" ValidChars="абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИКЛМНОПРСТУФХЦЧШЩЭЮЯ_-./|\,( ):$*#%;" />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Единицы">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label_Units_Item" runat="server" Text='<%# Eval("UNITS")%>' ToolTip="Единицы" Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox_Units_Edit" runat="server" Text='<%# Eval("UNITS")%>' ToolTip="Единицы" Width="80" ReadOnly="false" autofocus="true" placeholder="Единицы"></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="TextBox_Units_Edit_FTB" runat="server" FilterType="Numbers,Custom" TargetControlID="TextBox_Units_Edit" ValidChars="." />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Единицы приборов">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label_Cntr_Units_Item" runat="server" Text='<%# Eval("CNTR_UNITS")%>' ToolTip="Единицы приборов" Width="100px"></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox_Cntr_Units_Edit" runat="server" Text='<%# Eval("CNTR_UNITS")%>' ToolTip="Единицы приборов" Width="100" ReadOnly="false" autofocus="true" placeholder="Единицы приборов"></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="TextBox_Cntr_Units_Edit_FTB" runat="server" FilterType="Numbers,Custom" TargetControlID="TextBox_Cntr_Units_Edit" ValidChars="." />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Единицы нормы">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label_Norm_Units_Item" runat="server" Text='<%# Eval("NORM_UNITS")%>' ToolTip="Единицы нормы" Width="100px"></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox_Norm_Units_Edit" runat="server" Text='<%# Eval("NORM_UNITS")%>' ToolTip="Единицы нормы" Width="100" ReadOnly="false" autofocus="true" placeholder="Единицы нормы"></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="TextBox_Norm_Units_Edit_FTB" runat="server" FilterType="Numbers,Custom" TargetControlID="TextBox_Norm_Units_Edit" ValidChars="." />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Сальдо">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label_Saldo_Item" runat="server" Text='<%# Eval("SALDO")%>' ToolTip="Сальдо" Width="100px"></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox_Saldo_Edit" runat="server" Text='<%# Eval("SALDO")%>' ToolTip="Сальдо" Width="100" ReadOnly="false" autofocus="true" placeholder="Сальдо"></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="TextBox_Saldo_Edit_FTB" runat="server" FilterType="Numbers,Custom" TargetControlID="TextBox_Saldo_Edit" ValidChars="." />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Значение">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label_Value_Item" runat="server" Text='<%# Eval("VALUE")%>' ToolTip="Значение" Width="100px"></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox_Value_Edit" runat="server" Text='<%# Eval("VALUE")%>' ToolTip="Значение" Width="100" ReadOnly="false" autofocus="true" placeholder="Значение"></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="TextBox_Value_Edit_FTB" runat="server" FilterType="Numbers,Custom" TargetControlID="TextBox_Value_Edit" ValidChars="." />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Надбавка">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label_Added_Item" runat="server" Text='<%# Eval("ADDED")%>' ToolTip="Надбавка" Width="100px"></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox_Added_Edit" runat="server" Text='<%# Eval("ADDED")%>' ToolTip="Надбавка" Width="100" ReadOnly="false" autofocus="true" placeholder="Надбавка"></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="TextBox_Added_Edit_FTB" runat="server" FilterType="Numbers,Custom" TargetControlID="TextBox_Added_Edit" ValidChars="." />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Платеж">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label_Paid_Item" runat="server" Text='<%# Eval("PAID")%>' ToolTip="Платеж" Width="100px"></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox_Paid_Edit" runat="server" Text='<%# Eval("PAID")%>' ToolTip="Платеж" Width="100" ReadOnly="false" autofocus="true" placeholder="Платеж"></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="TextBox_Paid_Edit_FTB" runat="server" FilterType="Numbers,Custom" TargetControlID="TextBox_Paid_Edit" ValidChars="." />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Норм. коэфф.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label_Norm_Coeff_Item" runat="server" Text='<%# Eval("NORM_COEFF")%>' ToolTip="Норм. коэфф." Width="100px"></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox_Norm_Coeff_Edit" runat="server" Text='<%# Eval("NORM_COEFF")%>' ToolTip="Норм. коэфф." Width="100" ReadOnly="false" autofocus="true" placeholder="Норм. коэфф."></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="TextBox_Norm_Coeff_Edit_FTB" runat="server" FilterType="Numbers,Custom" TargetControlID="TextBox_Norm_Coeff_Edit" ValidChars="." />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Исп. норм. коэфф.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label_Use_Norm_Coeff_Item" runat="server" Text='<%# Eval("USE_NORM_COEFF")%>' ToolTip="Исп. норм. коэфф." Width="100px"></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox_Use_Norm_Coeff_Edit" runat="server" Text='<%# Eval("USE_NORM_COEFF")%>' ToolTip="Исп. норм. коэфф." Width="100" ReadOnly="false" autofocus="true" placeholder="Исп. норм. коэфф."></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="TextBox_Use_Norm_Coeff_Edit_FTB" runat="server" FilterType="Numbers,Custom" TargetControlID="TextBox_Use_Norm_Coeff_Edit" ValidChars="." />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Дни">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label_Pr_Days_Item" runat="server" Text='<%# Eval("PR_DAYS")%>' ToolTip="Дни" Width="100px"></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox_Pr_Days_Edit" runat="server" Text='<%# Eval("PR_DAYS")%>' ToolTip="Дни" Width="100" ReadOnly="false" autofocus="true" placeholder="Дни"></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="TextBox_Pr_Days_Edit_FTB" runat="server" FilterType="Numbers,Custom" TargetControlID="TextBox_Pr_Days_Edit" ValidChars="." />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Месяца">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label_Pr_Month_Item" runat="server" Text='<%# Eval("PR_MONTHS")%>' ToolTip="Месяца" Width="100px"></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox_Pr_Month_Edit" runat="server" Text='<%# Eval("PR_MONTHS")%>' ToolTip="Месяца" Width="100" ReadOnly="false" autofocus="true" placeholder="Месяца"></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="TextBox_Pr_Month_Edit_FTB" runat="server" FilterType="Numbers,Custom" TargetControlID="TextBox_Pr_Month_Edit" ValidChars="." />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Дата посл. платежа">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label_Calc_Last_Paid_Item" runat="server" Text='<%# Eval("LAST_PAID", "{0:dd.MM.yyyy}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox_Calc_Last_Paid_Edit" runat="server" Text='<%# Eval("LAST_PAID", "{0:yyyy-MM-dd}")%>' TextMode="Date" ToolTip="Дата посл. платежа" Width="120" ReadOnly="false" autofocus="true" placeholder="Дата посл. платежа"></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="TextBox_Calc_Last_Paid_Edit_FTB" runat="server" FilterType="Custom" TargetControlID="TextBox_Calc_Last_Paid_Edit" ValidChars="0123456789-:." />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Штрафы">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label_Penalties_Item" runat="server" Text='<%# Eval("PENALTIES")%>' ToolTip="Штрафы" Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox_Penalties_Edit" runat="server" Text='<%# Eval("PENALTIES")%>' ToolTip="Штрафы" Width="80" ReadOnly="false" autofocus="true" placeholder="Штрафы"></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="TextBox_Penalties_Edit_FTB" runat="server" FilterType="Numbers,Custom" TargetControlID="TextBox_Penalties_Edit" ValidChars="." />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ShowHeader="False" ItemStyle-HorizontalAlign="Left" ControlStyle-Width="18" ItemStyle-Wrap="false">
                                                                <EditItemTemplate>
                                                                    <asp:ImageButton ID="ImageButton_Calc_Update" runat="server" CausesValidation="True" CommandName="Update" ImageUrl="~/images/save.png" Text="Сохранить" ToolTip="Сохранить" Width="18px" />
                                                                    <asp:ImageButton ID="ImageButton_Calc_Cancel" runat="server" CausesValidation="True" CommandName="Cancel" ImageUrl="~/images/cancel.png" Text="Отменить" ToolTip="Отменить" Width="18px" />
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="ImageButton_Calc_Edit" runat="server" CausesValidation="False" CommandName="Edit" ImageUrl="~/images/edit.png" Text="Изменить" ToolTip="Изменить" Width="18px" />
                                                                    <asp:ImageButton ID="ImageButton_Calc_Delete" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/images/delete.png" Text="Удалить" ToolTip="Удалить" Width="18px" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>
                                                <br />
                                                <asp:Button ID="Button_Calc_Add" CssClass="Btn" runat="server" Text="Добавить" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>

                                <%-- ---------------------------------------Вкладка "История начислений"--------------------------------------------------------------------%>
                                <ajaxToolkit:TabPanel ID="TabPanel_Calc_History" runat="server" HeaderText="История начислений" ToolTip="История начислений">
                                    <HeaderTemplate>
                                        <p><span class="glyphicon glyphicon-book"></span>&nbsp;История начислений</p>
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                        <asp:UpdatePanel ID="UpdatePanel_Calc_History" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <h4><asp:Label ID="Label_Calc_History_Header" runat="server" Text="История начислений"></asp:Label></h4>
                                                <hr />
                                                <asp:Label ID="Label_Calc_History_Error" runat="server" Text="" ForeColor="Red" Font-Size="Large"></asp:Label>
                                                <asp:Panel ID="Panel_Calc_History" runat="server" ScrollBars="Auto">
                                                    <asp:GridView ID="GridView_Calc_History" AutoGenerateColumns="False" runat="server" CellPadding="1" ShowHeaderWhenEmpty="True" DataKeyNames="SERVICE_ID, FT_ID"  
                                                        Width="100%" AllowPaging="True" UseAccessibleHeader="False"
                                                        ForeColor="#666666" PageSize="20" EmptyDataText="Истории начислений не найдены" Font-Size="8pt">
                                                        <HeaderStyle BackColor="#EFEFEF" ForeColor="#FF6800" HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
                                                        <PagerStyle BackColor="#EFEFEF" CssClass="GridPager"></PagerStyle>
                                                        <RowStyle BorderColor="#FF6800" HorizontalAlign="Center"></RowStyle>
                                                        <Columns>
                                                            <asp:ButtonField ButtonType="Image" ImageUrl="Images/Doc.png" Text="Квитанции" CommandName="edit_click" ControlStyle-Width="18" />
                                                            <asp:BoundField HeaderText="Услуга" DataField="SERVICE_ID" ReadOnly="true" Visible="false" />
                                                            <asp:BoundField HeaderText="Фин. период" DataField="FT_ID" ReadOnly="true" Visible="false" />
                                                            <asp:TemplateField HeaderText="Тариф">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label_Calc_History_Tarif_Item" runat="server" Text='<%# Eval("TAR_NAME")%>' ToolTip="Тариф" Width="100px"></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox_Calc_History_Tarif_Edit" runat="server" Text='<%# Eval("TAR_NAME")%>' ToolTip="Тариф" Width="100" ReadOnly="true" autofocus="true" placeholder="Тариф"></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="TextBox_Calc_History_Tarif_Edit_FTB" runat="server" FilterType="Numbers,Custom" TargetControlID="TextBox_Calc_History_Tarif_Edit" ValidChars="абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИКЛМНОПРСТУФХЦЧШЩЭЮЯ .,_-:;(/|\)*+%#" />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Фин. период">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label_Calc_History_FT_Item" runat="server" Text='<%# Eval("FT_DATE")%>' ToolTip="Финансовый период" Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox_Calc_History_FT_Edit" runat="server" Text='<%# Eval("FT_DATE")%>' ToolTip="Финансовый период" Width="80" ReadOnly="true" autofocus="true" placeholder="Финансовый период"></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="TextBox_Calc_History_FT_Edit_FTB" runat="server" FilterType="Numbers, Custom" TargetControlID="TextBox_Calc_History_FT_Edit" ValidChars="абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИКЛМНОПРСТУФХЦЧШЩЭЮЯ ." />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Услуга">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label_Calc_History_Service_ID_Item" runat="server" Text='<%# Eval("Service_Name")%>' ToolTip="Услуга" Width="190px"></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox_Calc_History_Service_ID_Edit" runat="server" Text='<%# Eval("Service_Name")%>' ToolTip="Услуга" Width="150" ReadOnly="true" autofocus="true" placeholder="Услуга"></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="TextBox_Calc_History_Service_ID_Edit_FTB" runat="server" FilterType="Numbers,Custom" TargetControlID="TextBox_Calc_History_Service_ID_Edit" ValidChars="абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИКЛМНОПРСТУФХЦЧШЩЭЮЯ .,_-:(/|\)" />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Единицы">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label_Calc_History_Units_Item" runat="server" Text='<%# Eval("UNITS")%>' ToolTip="Единицы" Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox_Calc_History_Units_Edit" runat="server" Text='<%# Eval("UNITS")%>' ToolTip="Единицы" Width="80" ReadOnly="false" autofocus="true" placeholder="Единицы"></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="TextBox_Calc_History_Units_Edit_FTB" runat="server" FilterType="Numbers,Custom" TargetControlID="TextBox_Calc_History_Units_Edit" ValidChars="." />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Единицы приборов">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label_Calc_History_Cntr_Units_Item" runat="server" Text='<%# Eval("CNTR_UNITS")%>' ToolTip="Единицы приборов" Width="100px"></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox_Calc_History_Cntr_Units_Edit" runat="server" Text='<%# Eval("CNTR_UNITS")%>' ToolTip="Единицы приборов" Width="100" ReadOnly="false" autofocus="true" placeholder="Единицы приборов"></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="TextBox_Calc_History_Cntr_Units_Edit_FTB" runat="server" FilterType="Numbers,Custom" TargetControlID="TextBox_Calc_History_Cntr_Units_Edit" ValidChars="." />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Единицы нормы">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label_Calc_History_Norm_Units_Item" runat="server" Text='<%# Eval("NORM_UNITS")%>' ToolTip="Единицы нормы" Width="100px"></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox_Calc_History_Norm_Units_Edit" runat="server" Text='<%# Eval("NORM_UNITS")%>' ToolTip="Единицы нормы" Width="100" ReadOnly="false" autofocus="true" placeholder="Единицы нормы"></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="TextBox_Calc_History_Norm_Units_Edit_FTB" runat="server" FilterType="Numbers,Custom" TargetControlID="TextBox_Calc_History_Norm_Units_Edit" ValidChars="." />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Сальдо">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label_Calc_History_Saldo_Item" runat="server" Text='<%# Eval("SALDO")%>' ToolTip="Сальдо" Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox_Calc_History_Saldo_Edit" runat="server" Text='<%# Eval("SALDO")%>' ToolTip="Сальдо" Width="80" ReadOnly="false" autofocus="true" placeholder="Сальдо"></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="TextBox_Calc_History_Saldo_Edit_FTB" runat="server" FilterType="Numbers,Custom" TargetControlID="TextBox_Calc_History_Saldo_Edit" ValidChars="." />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Значение">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label_Calc_History_Value_Item" runat="server" Text='<%# Eval("VALUE")%>' ToolTip="Значение" Width="100px"></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox_Calc_History_Value_Edit" runat="server" Text='<%# Eval("VALUE")%>' ToolTip="Значение" Width="100" ReadOnly="false" autofocus="true" placeholder="Значение"></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="TextBox_Calc_History_Value_Edit_FTB" runat="server" FilterType="Numbers,Custom" TargetControlID="TextBox_Calc_History_Value_Edit" ValidChars="." />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Надбавка">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label_Calc_History_Added_Item" runat="server" Text='<%# Eval("ADDED")%>' ToolTip="Надбавка" Width="100px"></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox_Calc_History_Added_Edit" runat="server" Text='<%# Eval("ADDED")%>' ToolTip="Надбавка" Width="100" ReadOnly="false" autofocus="true" placeholder="Надбавка"></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="TextBox_Calc_History_Added_Edit_FTB" runat="server" FilterType="Numbers,Custom" TargetControlID="TextBox_Calc_History_Added_Edit" ValidChars="." />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Платеж">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label_Calc_History_Paid_Item" runat="server" Text='<%# Eval("PAID")%>' ToolTip="Платеж" Width="100px"></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox_Calc_History_Paid_Edit" runat="server" Text='<%# Eval("PAID")%>' ToolTip="Платеж" Width="100" ReadOnly="false" autofocus="true" placeholder="Платеж"></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="TextBox_Calc_History_Paid_Edit_FTB" runat="server" FilterType="Numbers,Custom" TargetControlID="TextBox_Calc_History_Paid_Edit" ValidChars="." />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Норм. коэфф.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label_Calc_History_Norm_Coeff_Item" runat="server" Text='<%# Eval("NORM_COEFF")%>' ToolTip="Норм. коэфф." Width="100px"></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox_Calc_History_Norm_Coeff_Edit" runat="server" Text='<%# Eval("NORM_COEFF")%>' ToolTip="Норм. коэфф." Width="100" ReadOnly="false" autofocus="true" placeholder="Норм. коэфф."></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="TextBox_Calc_History_Norm_Coeff_Edit_FTB" runat="server" FilterType="Numbers,Custom" TargetControlID="TextBox_Calc_History_Norm_Coeff_Edit" ValidChars="." />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Штрафы">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label_Calc_History_Penalties_Item" runat="server" Text='<%# Eval("PENALTIES")%>' ToolTip="Штрафы" Width="100px"></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox_Calc_History_Penalties_Edit" runat="server" Text='<%# Eval("PENALTIES")%>' ToolTip="Штрафы" Width="100" ReadOnly="false" autofocus="true" placeholder="Штрафы"></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="TextBox_Calc_History_Penalties_Edit_FTB" runat="server" FilterType="Numbers,Custom" TargetControlID="TextBox_Calc_History_Penalties_Edit" ValidChars="." />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ShowHeader="False" ItemStyle-HorizontalAlign="Left" ControlStyle-Width="18" ItemStyle-Wrap="false">
                                                                <EditItemTemplate>
                                                                    <asp:ImageButton ID="ImageButton_Calc_History_Update" runat="server" CausesValidation="True" CommandName="Update" ImageUrl="~/images/save.png" Text="Сохранить" ToolTip="Сохранить" Width="18px" />
                                                                    <asp:ImageButton ID="ImageButton_Calc_History_Cancel" runat="server" CausesValidation="True" CommandName="Cancel" ImageUrl="~/images/cancel.png" Text="Отменить" ToolTip="Отменить" Width="18px" />
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="ImageButton_Calc_History_Edit" runat="server" CausesValidation="False" CommandName="Edit" ImageUrl="~/images/edit.png" Text="Изменить" ToolTip="Изменить" Width="18px" />
                                                                    <asp:ImageButton ID="ImageButton_Calc_History_Delete" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/images/delete.png" Text="Удалить" ToolTip="Удалить" Width="18px" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>
                                                <br />
                                                <asp:Button ID="Button_Calc_History_Add" CssClass="Btn" runat="server" Text="Добавить" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>

                                <%-- ---------------------------------------Вкладка "Новая вкладка"-------------------------------------------------------------------------%>
                                <ajaxToolkit:TabPanel ID="TabPanel_New" runat="server" HeaderText="Новая" ToolTip="Новая">
                                    <HeaderTemplate>
                                        <p><span class="glyphicon glyphicon-flag"></span>&nbsp;Новая</p>
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                        <asp:UpdatePanel ID="UpdatePanel_New" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <h4>
                                                    <asp:Label ID="Label_New_Header" runat="server" Text="Новая вкладка"></asp:Label></h4>
                                                <hr />
                                                <asp:Label ID="Label_New_Error" runat="server" Text="" ForeColor="Red" Font-Size="Large"></asp:Label>
                                                <asp:Panel ID="Panel_New" runat="server" ScrollBars="Auto">
                                                    <asp:GridView ID="GridView_New" AutoGenerateColumns="False" runat="server" CellPadding="1" ShowHeaderWhenEmpty="True" DataKeyNames="NEW_ID"
                                                        Width="100%" AllowPaging="True" UseAccessibleHeader="False"
                                                        ForeColor="#666666" PageSize="20" EmptyDataText="Не найдены" Font-Size="8pt">
                                                        <HeaderStyle BackColor="#EFEFEF" ForeColor="#FF6800" HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
                                                        <PagerStyle BackColor="#EFEFEF" CssClass="GridPager"></PagerStyle>
                                                        <RowStyle BorderColor="#FF6800" HorizontalAlign="Center"></RowStyle>
                                                        <Columns>
                                                            <asp:ButtonField ButtonType="Image" ImageUrl="Images/Doc.png" Text="Квитанции" CommandName="edit_click" ControlStyle-Width="18" />
                                                            <asp:BoundField HeaderText="Услуга" DataField="SERVICE_ID" ReadOnly="true" Visible="false" />
                                                            <asp:TemplateField HeaderText="Тариф">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label_Tarif_Item" runat="server" Text='<%# Eval("TAR_NAME")%>' ToolTip="Тариф" Width="160px"></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox_Tarif_Edit" runat="server" Text='<%# Eval("TAR_NAME")%>' ToolTip="Тариф" Width="160" ReadOnly="true" autofocus="true" placeholder="Тариф"></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="TextBox_Tarif_Edit_FTB" runat="server" FilterType="Numbers,Custom" TargetControlID="TextBox_Tarif_Edit" ValidChars="абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИКЛМНОПРСТУФХЦЧШЩЭЮЯ_-./|\,( ):$*#%;" />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Дата посл. платежа">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label_Calc_Last_Paid_Item" runat="server" Text='<%# Eval("LAST_PAID", "{0:dd.MM.yyyy}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox_Calc_Last_Paid_Edit" runat="server" Text='<%# Eval("LAST_PAID", "{0:yyyy-MM-dd}")%>' TextMode="Date" ToolTip="Дата посл. платежа" Width="120" ReadOnly="false" autofocus="true" placeholder="Дата посл. платежа"></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="TextBox_Calc_Last_Paid_Edit_FTB" runat="server" FilterType="Custom" TargetControlID="TextBox_Calc_Last_Paid_Edit" ValidChars="0123456789-:." />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Штрафы">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label_Penalties_Item" runat="server" Text='<%# Eval("PENALTIES")%>' ToolTip="Штрафы" Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox_Penalties_Edit" runat="server" Text='<%# Eval("PENALTIES")%>' ToolTip="Штрафы" Width="80" ReadOnly="false" autofocus="true" placeholder="Штрафы"></asp:TextBox>
                                                                    <ajaxToolkit:FilteredTextBoxExtender ID="TextBox_Penalties_Edit_FTB" runat="server" FilterType="Numbers,Custom" TargetControlID="TextBox_Penalties_Edit" ValidChars="." />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ShowHeader="False" ItemStyle-HorizontalAlign="Left" ControlStyle-Width="18" ItemStyle-Wrap="false">
                                                                <EditItemTemplate>
                                                                    <asp:ImageButton ID="ImageButton_Calc_Update" runat="server" CausesValidation="True" CommandName="Update" ImageUrl="~/images/save.png" Text="Сохранить" ToolTip="Сохранить" Width="18px" />
                                                                    <asp:ImageButton ID="ImageButton_Calc_Cancel" runat="server" CausesValidation="True" CommandName="Cancel" ImageUrl="~/images/cancel.png" Text="Отменить" ToolTip="Отменить" Width="18px" />
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="ImageButton_Calc_Edit" runat="server" CausesValidation="False" CommandName="Edit" ImageUrl="~/images/edit.png" Text="Изменить" ToolTip="Изменить" Width="18px" />
                                                                    <asp:ImageButton ID="ImageButton_Calc_Delete" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/images/delete.png" Text="Удалить" ToolTip="Удалить" Width="18px" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>
                                                <br />
                                                <asp:Button ID="Button_New_Add" CssClass="Btn" runat="server" Text="Добавить" OnClick="Button_New_Add_Click" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>
                            </ajaxToolkit:TabContainer>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
        </asp:UpdatePanel>


        <%-- ---------------------------------------------------------------------------------------------------------------------------------------------------------------%>
        <%-- ------------------------------------------------------------------МОДАЛЬНЫЕ ОКНА-------------------------------------------------------------------------------%>
        <%-- ---------------------------------------------------------------------------------------------------------------------------------------------------------------%>


        <%-- ------------------------------------------------------------------Модальное окно "Добавить Л/С" AddOCC_ID------------------------------------------------------%>
        <div class="modal fade bd-example-modal-lg" id="myModalAddOCC_ID" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <asp:UpdatePanel ID="UpdatePanel_AddOCC_ID" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header" style="border-top-left-radius: 10px; color: #0033cc;">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                                <center><h4 class="modal-title"><asp:Label ID="lblModalOCC_ID" runat="server" Text=""></asp:Label></h4></center>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="TextBoxSQALL" class="form-control" placeholder="Площадь общая" autofocus="autofocus" runat="server"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="TextBoxSQALL" FilterType="Numbers,Custom" ValidChars="." />
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="TextBoxSQLIving" class="form-control" placeholder="Площадь жилая" runat="server"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="TextBoxSQLIving" FilterType="Numbers,Custom" ValidChars="." />
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="TextBoxSQMOP" class="form-control" placeholder="Площадь МОП" runat="server"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="TextBoxSQMOP" FilterType="Numbers,Custom" ValidChars="." />
                                    </div>
                                </div>
                                <br />
                                <asp:DropDownList ID="DropDownListPRIV" class="form-control" runat="server">
                                    <asp:ListItem Text="Приватизированная/собственность" Value="прив" />
                                    <asp:ListItem Text="Неприватизированная" Value="непр" />
                                </asp:DropDownList>
                                <br />
                                <asp:DropDownList ID="DropDownListOCC_TYPE" class="form-control" runat="server">
                                    <asp:ListItem Text="Отдельная" Value="1" />
                                    <asp:ListItem Text="Коммунальная" Value="2" />
                                </asp:DropDownList>
                                <br />
                                <asp:DropDownList ID="DropDownListOCC_STATUS" class="form-control" runat="server">
                                    <asp:ListItem Text="Жилое помещение" Value="1" />
                                    <asp:ListItem Text="Нежилое помещение" Value="2" />
                                </asp:DropDownList>
                                <br />
                                <asp:TextBox ID="TextBoxLName" class="form-control" placeholder="Фамилия" runat="server"></asp:TextBox>
                                <br />
                                <asp:TextBox ID="TextBoxFName" class="form-control" placeholder="Имя" runat="server"></asp:TextBox>
                                <br />
                                <asp:TextBox ID="TextBoxSNmame" class="form-control" placeholder="Отчество" runat="server"></asp:TextBox>
                                <br />
                                <div class="row">
                                    <div class="col-xs-6">
                                        <div class="input-group">
                                            <asp:TextBox ID="TextBoxB_Date" class="form-control" placeholder="Дата рождения" runat="server" TextMode="Date" aria-describedby="sizing-addon1"></asp:TextBox>
                                            <span class="input-group-addon" id="sizing-addon1">Дата рожд.</span>
                                        </div>
                                    </div>
                                    <div class="col-xs-6">
                                        <div class="input-group">
                                            <asp:TextBox ID="TextBoxDateReg" class="form-control" placeholder="Дата регистрации" runat="server" TextMode="Date" aria-describedby="sizing-addon2"></asp:TextBox>
                                            <span class="input-group-addon" id="sizing-addon2">Дата рег.</span>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="DropDownListP_Status" class="form-control" runat="server">
                                            <asp:ListItem Text="Владелец/Наниматель" Value="4" />
                                            <asp:ListItem Text="Постоянная прописка" Value="1" />
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="DropDownListSexStatus" class="form-control" runat="server">
                                            <asp:ListItem Text="Мужской пол" Value="1" />
                                            <asp:ListItem Text="Женский пол" Value="2" />
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <asp:Button ID="ButtonAddOCC_ID" class="btn btn-default  form-control" runat="server" Text="Добавить" OnClick="ButtonAddOCC_ID_Click" />
                                <div class="alert-danger" style="margin-top: 10px;">
                                    <asp:Label ID="Label_Add_OCC" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>

        <%-------------------------------------------------------------------- Модальное окно "Поиск по фамилии" SEARCH_OCC_FAMILIA-----------------------------------------%>
        <div class="modal fade bd-example-modal-lg" id="myModalSEARCH_OCC_FAMILIA" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header" style="border-top-left-radius: 10px; color: #0033cc;">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                                <center><h4 class="modal-title"><asp:Label ID="Label3" runat="server" Text=""></asp:Label></h4></center>
                            </div>
                            <div class="modal-body">
                                <asp:Label ID="Label4" runat="server" Text="Введите фамилию, имя и отчество, например Васильев В В"></asp:Label><br />
                                <br />
                                <div class="row">
                                    <div class="col-md-3">
                                        <asp:TextBox ID="TextBox5" runat="server" Width="110px"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="TextBox6" runat="server" Width="110px"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="TextBox7" runat="server" Width="110px"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <div style="text-align: right">
                                            <asp:Button ID="Button1" runat="server" Text="Найти" Width="110px" />
                                        </div>
                                    </div>

                                </div>
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <br />
                                        <br />
                                        <asp:GridView ID="GridView3" AutoGenerateColumns="False" runat="server" CellPadding="2" ShowHeaderWhenEmpty="True" DataKeyNames="ID"
                                            Width="100%" AllowPaging="True" UseAccessibleHeader="False"
                                            ForeColor="#666666" PageSize="20" EmptyDataText="Человек не найден">
                                            <HeaderStyle BackColor="#EFEFEF" ForeColor="#FF6800" HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
                                            <PagerStyle BackColor="#EFEFEF" CssClass="GridPager"></PagerStyle>
                                            <RowStyle BorderColor="#FF6800" HorizontalAlign="Center"></RowStyle>
                                            <Columns>
                                                <asp:ButtonField ButtonType="Image" ImageUrl="Images/Button.png" Text="Выбрать" CommandName="edit_click" />
                                                <asp:BoundField HeaderText="Лицевой счет" DataField="ID" />
                                                <asp:BoundField HeaderText="Ответственный" DataField="OTVL" />
                                                <asp:BoundField HeaderText="Адрес" DataField="Addr" />
                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
                
        <%-------------------------------------------------------------------- Модальное окно "Архивация Л/С" Arhiv_LS------------------------------------------------------%>
        <div class="modal fade bd-example-modal-lg" id="myModal_Arhiv_LS" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <asp:UpdatePanel ID="UpdatePanel_myModal_Arhiv_LS" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-content" style="width: 550px">
                            <div class="modal-header" style="border-top-left-radius: 10px; color: #0033cc;">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                                <center><h4 class="modal-title"><asp:Label ID="Label_myModal_Arhiv_LS_header" runat="server" Text="Архивирование лицевого счета"></asp:Label></h4></center>
                            </div>
                            <div class="modal-body" style="text-align: center;">
                                <div class="row" style="text-align: center; width: 100%; vertical-align: central;">
                                    <asp:Label ID="Label7" runat="server" ForeColor="Red" Font-Size="Large" Font-Bold="true" Text="!!!Внимание!!!"></asp:Label><br />
                                    <asp:Label ID="Label6" runat="server" ForeColor="Red" Text="Архивация является необратимой операцией!!!"></asp:Label><br />
                                    <asp:Label ID="Label_Reason_Arhiv" runat="server" Text="Укажите причину"></asp:Label><br />
                                    <br />
                                    <asp:TextBox ID="TextBox_Reason_arhiv" runat="server" TextMode="MultiLine" Style="resize: none; height: 100px; width: 400px;"></asp:TextBox><br />
                                    <br />

                                    <button id="Button_myModal_Arhiv_LS" runat="server" type="button" class="btn-default" style="color: #ff6800" onserverclick="Button_myModal_Arhiv_LS_Click" visible="true">
                                        <span aria-hidden="true" class="glyphicon glyphicon-floppy-disk"></span>
                                        В архив
                                    </button>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        


        <%-------------------------------------------------------------------- Модальное окно "Добавить человека" Add_User--------------------------------------------------%>
        <div class="modal fade bd-example-modal-lg" id="myModal_Add_User" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <asp:UpdatePanel ID="UpdatePanel_User_Add" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-content" style="width: 640px">
                            <div class="modal-header" style="border-top-left-radius: 10px; color: #0033cc;">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                                <center><h4 class="modal-title"><asp:Label ID="Label1" runat="server" Text="Регистрация человека"></asp:Label></h4></center>
                            </div>
                            <div class="modal-body" style="text-align: center;">
                                <div style="text-align: center">
                                    <asp:Label ID="Label_User_Add_Error1" runat="server" ForeColor="Red" Font-Size="Large" Font-Bold="true" Text=""></asp:Label><br />
                                    <asp:Label ID="Label_User_Add_Error2" runat="server" ForeColor="Red" Text=""></asp:Label><br />
                                    <br />
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="TextBox_Add_User_Surname" class="form-control" placeholder="Фамилия" autofocus="autofocus" runat="server"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="TextBox_Add_User_Surname" FilterType="Numbers,Custom" ValidChars="абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИКЛМНОПРСТУФХЦЧШЩЭЮЯ №-" />
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="TextBox_Add_User_Name" class="form-control" placeholder="Имя" runat="server"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="TextBox_Add_User_Name" FilterType="Numbers,Custom" ValidChars="абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИКЛМНОПРСТУФХЦЧШЩЭЮЯ №-" />
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="TextBox_Add_User_Patronymic" class="form-control" placeholder="Отчество" runat="server"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="TextBox_Add_User_Patronymic" FilterType="Numbers,Custom" ValidChars="абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИКЛМНОПРСТУФХЦЧШЩЭЮЯ №-" />
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-xs-6">
                                        <div class="input-group">
                                            <asp:TextBox ID="TextBox_User_Add_Date_Birth" class="form-control" placeholder="Дата рождения" runat="server" TextMode="Date" aria-describedby="sizing-addon_User_Add_Date_Birth"></asp:TextBox>
                                            <span class="input-group-addon" id="sizing-addon_User_Add_Date_Birth">Дата рожд.</span>
                                        </div>
                                    </div>
                                    <div class="col-xs-6">
                                        <div class="input-group">
                                            <asp:TextBox ID="TextBox_User_Add_Date_Reg" class="form-control" placeholder="Дата регистрации" runat="server" TextMode="Date" aria-describedby="sizing-addon_User_Add_Date_Reg"></asp:TextBox>
                                            <span class="input-group-addon" id="sizing-addon_User_Add_Date_Reg">Дата рег.</span>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-sm-8">
                                        <div class="input-group">
                                            <asp:DropDownList ID="DropDownList_User_Add_Reg_Type" class="form-control" runat="server" aria-describedby="sizing-addon_User_Add_Reg_Type" AutoPostBack="true">
                                            </asp:DropDownList>
                                            <span class="input-group-addon" id="sizing-addon_User_Add_Reg_Type">Тип рег.</span>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="input-group">
                                            <asp:DropDownList ID="DropDownList_User_Add_Sex" class="form-control" runat="server" aria-describedby="sizing-addon_User_Add_Sex">
                                            </asp:DropDownList>
                                            <span class="input-group-addon" id="sizing-addon_User_Add_Sex">Пол</span>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-sm-8">
                                        <div class="input-group">
                                            <asp:TextBox ID="TextBox_User_Add_End_Reg" Visible="false" class="form-control" placeholder="Окончание регистрации" runat="server" TextMode="Date" aria-describedby="sizing-addon_User_Add_End_Reg"></asp:TextBox>
                                            <span class="input-group-addon" id="sizing-addon_User_Add_End_Reg">Окончание рег.</span>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-sm-8">
                                        <div class="input-group">
                                            <asp:DropDownList ID="DropDownList_User_Add_Who_ID" class="form-control" runat="server" aria-describedby="sizing-addon_User_Add_Who_ID">
                                            </asp:DropDownList>
                                            <span class="input-group-addon" id="sizing-addon_User_Add_Who_ID">Статус</span>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <button id="Button_User_Add" runat="server" type="button" class=" btn btn-default" style="color: #ff6800" onserverclick="Button_User_Add_Click" visible="true">
                                    <span aria-hidden="true" class="glyphicon glyphicon-plus-sign"></span>
                                    Зарегистрировать
                                </button>
                                <div class="alert-danger" style="margin-top: 10px;">
                                    <asp:Label ID="Label_User_Add_Error" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                        </div>
                        </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        
        <%-------------------------------------------------------------------- Модальное окно "Удалить  человека" Delete_User-----------------------------------------------%>
        <div class="modal fade bd-example-modal-lg" id="myModal_delete_user" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <asp:UpdatePanel ID="UpdatePanel_User_Unreg" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-content" style="width: 550px">
                            <div class="modal-header" style="border-top-left-radius: 10px; color: #0033cc;">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                                <center><h4 class="modal-title"><asp:Label ID="Label8" runat="server" Text="Выписка человека"></asp:Label></h4></center>
                            </div>
                            <div class="modal-body" style="text-align: center;">
                                <div class="row" style="text-align: center; width: 100%; vertical-align: central; margin-left: 20px;">
                                    <asp:Label ID="Label_User_Unreg_Error_1" runat="server" ForeColor="Red" Font-Size="Large" Font-Bold="true" Text="!!!Внимание!!!"></asp:Label><br />
                                    <asp:Label ID="Label_User_Unreg_Error_2" runat="server" ForeColor="Red" Text="Выписка является необратимой операцией!!!"></asp:Label><br />
                                    <br />
                                    <div class="row">
                                        <div class="col-xs-6">
                                            <div class="input-group">
                                                <%--<asp:Label ID="Label11" runat="server" Text="Укажите дату выписки"></asp:Label><br />--%>
                                                <asp:TextBox ID="TextBox_Date_Unreg" class="form-control" runat="server" TextMode="Date" Style="width: 150px; resize: none;" aria-describedby="sizing-addon21"></asp:TextBox><br />
                                                <span class="input-group-addon" id="sizing-addon21">Дата выписки&nbsp;&nbsp;&nbsp;&nbsp;</span>
                                                <br />
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-xs-6">
                                            <div class="input-group">
                                                <asp:DropDownList ID="DropDownList_Unreg_status" runat="server" class="form-control" aria-describedby="sizing-addon22" Style="width: 150px; resize: none;">
                                                    <%--<asp:ListItem Text="Не указано" Value="????" />
                                                    <asp:ListItem Text="Продажа квартиры" Value="1001" />
                                                    <asp:ListItem Text="Выписан" Value="1002" />
                                                    <asp:ListItem Text="Выписан по обмену" Value="вобм" />
                                                    <asp:ListItem Text="Выписан по смерти" Value="умер" />
                                                    <asp:ListItem Text="Истечение прописки" Value="усе!" />--%>
                                                </asp:DropDownList><br />
                                                <span class="input-group-addon" id="sizing-addon22">Статус выписки&nbsp;</span>
                                                <br />
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-xs-6">
                                            <div class="input-group">

                                                <button id="Button_Unreg" runat="server" type="button" class=" btn btn-default" style="color: #ff6800" onserverclick="Button_Unreg_Click" visible="true">
                                                    <span aria-hidden="true" class="glyphicon glyphicon-remove"></span>
                                                    Выписать
                                                </button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>

        <%-------------------------------------------------------------------- Модальное окно "Добавить документ человека" Add_User_Doc-------------------------------------%>
        <div class="modal fade bd-example-modal-lg" id="myModal_add_user_doc" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <asp:UpdatePanel ID="UpdatePanel_User_Doc_Add" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-content" style="width: 550px">
                            <div class="modal-header" style="border-top-left-radius: 10px; color: #0033cc;">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                                <center><h4 class="modal-title"><asp:Label ID="Label_User_Doc_Add_Header" runat="server" Text="Добавление документа"></asp:Label></h4></center>
                            </div>
                            <div class="modal-body" style="text-align: center;">
                                <div class="row" style="text-align: center; width: 100%; vertical-align: central; margin-left: 20px;">
                                    <div class="row">
                                        <asp:Label ID="Label_Add_User_Doc_Error1" runat="server" ForeColor="Red" Font-Size="Large" Font-Bold="true" Text=""></asp:Label><br />
                                        <asp:Label ID="Label_Add_User_Doc_Error2" runat="server" ForeColor="Red" Text=""></asp:Label><br />
                                        <br />
                                        <div class="col-xs-6">
                                            <div class="input-group">
                                                <asp:DropDownList ID="DropDownList_User_Doc_Add_Type" CssClass="form-control" Style="width: 150px; resize: none;" runat="server" aria-describedby="sizing-addon30">
                                                    
                                                </asp:DropDownList>
                                                <span class="input-group-addon" id="sizing-addon30">Тип документа</span>
                                                <br />
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-xs-6">
                                            <div class="input-group">
                                                <asp:TextBox ID="TextBox_User_Doc_Add_Date" class="form-control" runat="server" TextMode="Date" Style="width: 150px; resize: none;" aria-describedby="sizing-addon31"></asp:TextBox><br />
                                                <span class="input-group-addon" id="sizing-addon31">Дата документа</span>
                                                <br />
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-xs-6">
                                            <div class="input-group">
                                                <asp:TextBox ID="TextBox_User_Doc_Add_Series" class="form-control" runat="server" Style="width: 150px; resize: none;" aria-describedby="sizing-addon32" MaxLength="4"></asp:TextBox><br />
                                                <ajaxToolkit:FilteredTextBoxExtender ID="TextBox_User_Doc_Add_Series_FTB" runat="server" FilterType="Numbers" TargetControlID="TextBox_User_Doc_Add_Series"  />
                                                <span class="input-group-addon" id="sizing-addon32">Серия документа</span>
                                                <br />
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-xs-6">
                                            <div class="input-group">
                                                <asp:TextBox ID="TextBox_User_Doc_Add_Number" class="form-control" runat="server" Style="width: 150px; resize: none;" aria-describedby="sizing-addon33" MaxLength="6"></asp:TextBox><br />
                                                <ajaxToolkit:FilteredTextBoxExtender ID="TextBox_User_Doc_Add_Number_FTB" runat="server" FilterType="Numbers" TargetControlID="TextBox_User_Doc_Add_Number"  />
                                                <span class="input-group-addon" id="sizing-addon33">Номер документа</span>
                                                <br />
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-xs-6">
                                            <div class="input-group">
                                                <asp:TextBox ID="TextBox_User_Doc_Add_Organization" class="form-control" runat="server" Style="width: 150px; resize: none;" aria-describedby="sizing-addon34" MaxLength="100"></asp:TextBox><br />
                                                <ajaxToolkit:FilteredTextBoxExtender ID="TextBox_User_Doc_Add_Organization_FTB" runat="server" FilterType="Numbers,Custom" TargetControlID="TextBox_User_Doc_Add_Organization" ValidChars="АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя ,.-"  />
                                                <span class="input-group-addon" id="sizing-addon34">Кем выдан</span>
                                                <br />
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                     <div class="row">
                                        <div class="col-xs-6">
                                            <div class="input-group">
                                                <asp:TextBox ID="TextBox_User_Doc_Add_Code" class="form-control" runat="server" Style="width: 150px; resize: none;" aria-describedby="sizing-addon35" MaxLength="7"></asp:TextBox><br />
                                                <ajaxToolkit:FilteredTextBoxExtender ID="TextBox_User_Doc_Add_Code_FTB" runat="server" FilterType="Custom,Numbers" TargetControlID="TextBox_User_Doc_Add_Code" ValidChars="-"  />
                                                <span class="input-group-addon" id="sizing-addon35">Код подразделения</span>
                                                <br />
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-xs-6">
                                            <div class="input-group">
                                                <button id="Button_User_Doc_Add" runat="server" type="button" class=" btn btn-default" style="color: #ff6800" onserverclick="Button_User_Doc_Add_Click" visible="true">
                                                    <span aria-hidden="true" class="glyphicon glyphicon-plus-sign"></span>
                                                    Добавить
                                                </button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>

        <%-------------------------------------------------------------------- Модальное окно "Удалить  документ человека" Delete_User_Doc----------------------------------%>
        <div class="modal fade bd-example-modal-lg" id="myModal_delete_user_doc" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <asp:UpdatePanel ID="UpdatePanel_User_doc_delete" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-content" style="width: 550px">
                            <div class="modal-header" style="border-top-left-radius: 10px; color: #0033cc;">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                                <center><h4 class="modal-title"><asp:Label ID="Label9" runat="server" Text="Удаление документа человека"></asp:Label></h4></center>
                            </div>
                            <div class="modal-body" style="text-align: center;">
                                <div class="row" style="text-align: center; width: 100%; vertical-align: central; margin-left: 20px;">
                                    <asp:Label ID="Label_User_Doc_Delete_Error1" runat="server" ForeColor="Red" Font-Size="Large" Font-Bold="true" Text="!!!Внимание!!!"></asp:Label><br />
                                    <asp:Label ID="Label_User_Doc_Delete_Error2" runat="server" ForeColor="Red" Text="Вы уверены, что хотите удалить данный документ?"></asp:Label><br />
                                    <br />

                                    <button id="Button5" runat="server" type="button" class=" btn btn-default" style="color: #ff6800" onserverclick="Button_Doc_Del_Click" visible="true">
                                        <span aria-hidden="true" class="glyphicon glyphicon-remove"></span>
                                        Удалить
                                    </button>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>


        
        <%-------------------------------------------------------------------- Модальное окно "Добавить прибор" Counter_Add-------------------------------------------------%>
        <div class="modal fade bd-example-modal-lg" id="myModal_Counter_Add" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <asp:UpdatePanel ID="UpdatePanel_Counter_Add" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-content" style="width: 800px; padding-right:20px;">
                            <div class="modal-header" style="border-top-left-radius: 10px; color: #0033cc;">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                                <center><h4 class="modal-title"><asp:Label ID="Label_Header_Counter_Add" runat="server" Text="Добавление прибора учета"></asp:Label></h4></center>
                            </div>
                            <div class="modal-body">
                                <div style="text-align: center">
                                    <asp:Label ID="Label_Counter_Add_Error1" runat="server" ForeColor="Red" Font-Size="Large" Font-Bold="true" Text=""></asp:Label><br />
                                    <asp:Label ID="Label_Counter_Add_Error2" runat="server" ForeColor="Red" Text=""></asp:Label><br />
                                    <br />
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="input-group">
                                            <asp:DropDownList ID="DropDownList_Counter_Add_Model" runat="server" class="form-control" Width="170" aria-describedby="sizing-addon_Counter_Add_Model" AutoPostBack="true"></asp:DropDownList>
                                            <span class="input-group-addon" id="sizing-addon_Counter_Add_Model">Модель прибора&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-group">
                                            <asp:TextBox ID="TextBox_Counter_Add_Serial_Number" class="form-control" runat="server" placeholder="Серийный номер" Width="170px" aria-describedby="sizing-addon_Counter_Add_Serial_Number"></asp:TextBox>
                                            <span class="input-group-addon" id="sizing-addon_Counter_Add_Serial_Number">Серийный номер прибора</span>
                                        </div>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_Counter_Add_Serial_Number" runat="server" TargetControlID="TextBox_Counter_Add_Serial_Number" FilterType="Custom" ValidChars="0123456789-/АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюяabcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ" />
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="input-group">
                                            <asp:TextBox ID="TextBox_Counter_Add_Service" class="form-control" runat="server" placeholder="Услуга" Width="170px" ReadOnly="true" aria-describedby="sizing-addon_Counter_Add_Service"></asp:TextBox>
                                            <span class="input-group-addon" id="sizing-addon_Counter_Add_Service">Услуга&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-group">
                                            <asp:TextBox ID="TextBox_Counter_Add_Current" class="form-control" runat="server" placeholder="Текущие показания" Width="170px" aria-describedby="sizing-addon_Counter_Add_Current"></asp:TextBox>
                                            <span class="input-group-addon" id="sizing-addon_Counter_Add_Current">Текущие показания&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
                                        </div>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="TextBox_Counter_Add_Current" FilterType="Custom" ValidChars="0123456789" />
                                    </div>
                               </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-6" >
                                        <div class="input-group">
                                            <%--<asp:TextBox ID="TextBox_Counter_Add_Type" class="form-control" runat="server" placeholder="Тип прибора" Width="170px" ReadOnly="true" aria-describedby="sizing-addon_Counter_Add_Type"></asp:TextBox>--%>
                                            <asp:DropDownList ID="DropDownList_Counter_Add_Type" runat="server" class="form-control" Width="170" aria-describedby="sizing-addon_Counter_Add_Type" ReadOnly="true"></asp:DropDownList>
                                            <span class="input-group-addon" id="sizing-addon_Counter_Add_Type">Тип прибора&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span> 
                                       </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-group">
                                            <asp:TextBox ID="TextBox_Counter_Add_Last" class="form-control" runat="server" placeholder="Последние показания" Width="170px" MaxLength="15" aria-describedby="sizing-addon_Counter_Add_Last"></asp:TextBox>
                                            <span class="input-group-addon" id="sizing-addon_Counter_Add_Last">Последние показания&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span> 
                                        </div>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="TextBox_Counter_Add_Last" FilterType="Custom" ValidChars="0123456789" />
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="input-group">
                                            <asp:TextBox ID="TextBox_Counter_Add_Max" class="form-control" runat="server" placeholder="Макс. показания" Width="170px" ReadOnly="true" aria-describedby="sizing-addon_Counter_Add_Max"></asp:TextBox>
                                            <span class="input-group-addon" id="sizing-addon_Counter_Add_Max">Максимальные показания</span>
                                        </div>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_Counter_Add_Max" runat="server" TargetControlID="TextBox_Counter_Add_Max" FilterType="Custom" ValidChars="0123456789." />
                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-group">
                                            <asp:TextBox ID="TextBox_Counter_Add_Date_Install" class="form-control" runat="server" Width="170px"  TextMode="Date" aria-describedby="sizing-addon_Counter_Add_Date_Install" AutoPostBack="true"></asp:TextBox>
                                            <span class="input-group-addon" id="sizing-addon_Counter_Add_Date_Install">Дата установки прибора&nbsp;</span>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="input-group">
                                            <asp:TextBox ID="TextBox_Counter_Add_Koeff" class="form-control" runat="server" placeholder="Коэффициент" Width="170px" ReadOnly="true" aria-describedby="sizing-addon_Counter_Add_Koeff"></asp:TextBox>
                                            <span class="input-group-addon" id="sizing-addon_Counter_Add_Koeff">Коэффициент&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
                                        </div>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_Counter_Add_Koeff" runat="server" TargetControlID="TextBox_Counter_Add_Koeff" FilterType="Custom" ValidChars="0123456789." />
                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-group">
                                            <asp:TextBox ID="TextBox_Counter_Add_Date_Comission" class="form-control" runat="server" Width="170px"  TextMode="Date" aria-describedby="sizing-addon_Counter_Add_Date_Comission"></asp:TextBox>
                                            <span class="input-group-addon" id="sizing-addon_Counter_Add_Date_Comission">Дата ввода в эксплуатацию</span>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="input-group">
                                            <asp:TextBox ID="TextBox_Counter_Add_Date_Of_Last_Verification" class="form-control" runat="server" Width="170px" ReadOnly="false" TextMode="Date" aria-describedby="sizing-addon_Counter_Add_Date_Of_Last_Verification"></asp:TextBox>
                                            <span class="input-group-addon" id="sizing-addon_Counter_Add_Date_Of_Last_Verification">Дата следующей поверки&nbsp;</span>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-group">
                                            <asp:TextBox ID="TextBox_Counter_Add_Date_Plant_Mark" class="form-control" runat="server" Width="170px" TextMode="Date" aria-describedby="sizing-addon_Counter_Add_Date_Plant_Mark"></asp:TextBox>
                                            <span class="input-group-addon" id="sizing-addon_Counter_Add_Date_Plant_Mark">Дата пломбировки&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div style="text-align: center">
                                            <button id="Button_Counter_Add_Complete" runat="server" type="button" class="btn btn-default" style="color: #ff6800" onserverclick="Button_Counter_Add_Complete_Click" visible="true">
                                                <span aria-hidden="true" class="glyphicon glyphicon-plus-sign"></span>
                                                Добавить
                                            </button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
                
        <%-------------------------------------------------------------------- Модальное окно "Удалить  прибор" Delete_Counter----------------------------------------------%>
        <div class="modal fade bd-example-modal-lg" id="myModal_Counter_delete" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <asp:UpdatePanel ID="UpdatePanel_Counter_delete" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-content" style="width: 550px">
                            <div class="modal-header" style="border-top-left-radius: 10px; color: #0033cc;">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                                <center><h4 class="modal-title"><asp:Label ID="Label_Counter_Dell_Header" runat="server" Text="Удаление прибора учета"></asp:Label></h4></center>
                            </div>
                            <div class="modal-body" style="text-align: center;">
                                <div class="row" style="text-align: center; width: 100%; vertical-align: central; margin-left: 20px;">
                                    <asp:Label ID="Label_Counter_Delete_Error1" runat="server" ForeColor="Red" Font-Size="Large" Font-Bold="true" Text="!!!Внимание!!!"></asp:Label><br />
                                    <asp:Label ID="Label_Counter_Delete_Error2" runat="server" ForeColor="Red" Text="Вы уверены, что хотите удалить данный прибор?"></asp:Label><br />
                                    <br />
                                    <button id="Button_Counter_Dell" runat="server" type="button" class=" btn btn-default" style="color: #ff6800" onserverclick="Button_Counter_Dell_Click" visible="true">
                                        <span aria-hidden="true" class="glyphicon glyphicon-remove"></span>
                                        Удалить
                                    </button>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>


        
        <%-------------------------------------------------------------------- Модальное окно "Добавить начисления" Calc_Add------------------------------------------------%>
        <div class="modal fade bd-example-modal-lg" id="myModal_Calc_Add" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <asp:UpdatePanel ID="UpdatePanel_Calc_Add" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-content" style="width: 640px">
                            <div class="modal-header" style="border-top-left-radius: 10px; color: #0033cc;">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                                <center><h4 class="modal-title"><asp:Label ID="Label_Header_Calc_Add" runat="server" Text="Добавление начислений"></asp:Label></h4></center>
                            </div>
                            <div class="modal-body">
                                <div style="text-align: center">
                                    <asp:Label ID="Label_Calc_Add_Error1" runat="server" ForeColor="Red" Font-Size="Large" Font-Bold="true" Text=""></asp:Label><br />
                                    <asp:Label ID="Label_Calc_Add_Error2" runat="server" ForeColor="Red" Text=""></asp:Label><br />
                                    <br />
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="input-group">
                                            <asp:DropDownList ID="DropDownList_Calc_Add_Tarif" runat="server" class="form-control" Width="380" aria-describedby="sizing-addon_Calc_Calc_Add_Tarif" AutoPostBack="true"></asp:DropDownList>
                                            <span class="input-group-addon" id="sizing-addon_Calc_Calc_Add_Tarif">Тариф&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="input-group">
                                            <asp:DropDownList ID="DropDownList_Calc_Add_Service" runat="server" class="form-control" Width="380" aria-describedby="sizing-addon_Calc_Add_Service" AutoPostBack="true"></asp:DropDownList>
                                            <span class="input-group-addon" id="sizing-addon_Calc_Add_Service">Услуга&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-4">
                                        <asp:TextBox ID="TextBox_Calc_Add_Norm_Coeff" class="form-control" runat="server" placeholder="Норм. коэфф." Width="170px"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_Calc_Add_Norm_Coeff" runat="server" TargetControlID="TextBox_Calc_Add_Norm_Coeff" FilterType="Custom" ValidChars="0123456789." />
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="TextBox_Calc_Add_Units" class="form-control" runat="server" placeholder="Единицы" Width="170px" MaxLength="15"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_Calc_Add_Units" runat="server" TargetControlID="TextBox_Calc_Add_Units" FilterType="Custom" ValidChars="0123456789." />
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="TextBox_Calc_Add_Cntr_Units" class="form-control" runat="server" placeholder="Ед. счетчика" Width="170px"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_Calc_Add_Cntr_Units" runat="server" TargetControlID="TextBox_Calc_Add_Cntr_Units" FilterType="Custom" ValidChars="0123456789." />
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-4">
                                        <asp:TextBox ID="TextBox_Calc_Add_Norm_Units" class="form-control" runat="server" placeholder="Ед. нормы" Width="170px"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_Calc_Add_Norm_Units" runat="server" TargetControlID="TextBox_Calc_Add_Norm_Units" FilterType="Custom" ValidChars="0123456789." />
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="TextBox_Calc_Add_Saldo" class="form-control" runat="server" placeholder="Сальдо" Width="170px" MaxLength="15"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_Calc_Add_Saldo" runat="server" TargetControlID="TextBox_Calc_Add_Saldo" FilterType="Custom" ValidChars="0123456789." />
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="TextBox_Calc_Add_Value" class="form-control" runat="server" placeholder="Значение" Width="170px"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_Calc_Add_Value" runat="server" TargetControlID="TextBox_Calc_Add_Value" FilterType="Custom" ValidChars="0123456789." />
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-4">
                                        <asp:TextBox ID="TextBox_Calc_Add_Added" class="form-control" runat="server" placeholder="Надбавка" Width="170px"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_Calc_Add_Added" runat="server" TargetControlID="TextBox_Calc_Add_Added" FilterType="Custom" ValidChars="0123456789." />
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="TextBox_Calc_Add_Paid" class="form-control" runat="server" placeholder="Платеж" Width="170px" MaxLength="15"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_Calc_Add_Paid" runat="server" TargetControlID="TextBox_Calc_Add_Paid" FilterType="Custom" ValidChars="0123456789." />
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="TextBox_Calc_Add_Penalties" class="form-control" runat="server" placeholder="Штраф" Width="170px"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_Calc_Add_Penalties" runat="server" TargetControlID="TextBox_Calc_Add_Penalties" FilterType="Custom" ValidChars="0123456789." />
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div style="text-align: center">
                                            <button id="Button_Calc_Add_Complete" runat="server" type="button" class="btn btn-default" style="color: #ff6800" onserverclick="Button_Calc_Add_Complete_Click" visible="true">
                                                <span aria-hidden="true" class="glyphicon glyphicon-plus-sign"></span>
                                                Добавить
                                            </button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        
        <%-------------------------------------------------------------------- Модальное окно "Удалить  начисления" Calc_Delete---------------------------------------------%>
        <div class="modal fade bd-example-modal-lg" id="myModal_Calc_delete" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <asp:UpdatePanel ID="UpdatePanel_Calc_delete" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-content" style="width: 550px">
                            <div class="modal-header" style="border-top-left-radius: 10px; color: #0033cc;">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                                <center><h4 class="modal-title"><asp:Label ID="Label_Calc_Dell_Header" runat="server" Text="Удаление начисления"></asp:Label></h4></center>
                            </div>
                            <div class="modal-body" style="text-align: center;">
                                <div class="row" style="text-align: center; width: 100%; vertical-align: central; margin-left: 20px;">
                                    <asp:Label ID="Label_Calc_Delete_Error1" runat="server" ForeColor="Red" Font-Size="Large" Font-Bold="true" Text="!!!Внимание!!!"></asp:Label><br />
                                    <asp:Label ID="Label_Calc_Delete_Error2" runat="server" ForeColor="Red" Text="Вы уверены, что хотите удалить данное начисление?"></asp:Label><br />
                                    <br />
                                    <button id="Button_Calc_Dell" runat="server" type="button" class=" btn btn-default" style="color: #ff6800" onserverclick="Button_Calc_Dell_Click" visible="true">
                                        <span aria-hidden="true" class="glyphicon glyphicon-remove"></span>
                                        Удалить
                                    </button>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>


        
        <%-------------------------------------------------------------------- Модальное окно "Добавить в историю начислений" Calc_History_Add------------------------------%>
        <div class="modal fade bd-example-modal-lg" id="myModal_Calc_History_Add" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <asp:UpdatePanel ID="UpdatePanel_Calc_History_Add" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-content" style="width: 640px">
                            <div class="modal-header" style="border-top-left-radius: 10px; color: #0033cc;">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                                <center><h4 class="modal-title"><asp:Label ID="Label_Header_Calc_History_Add" runat="server" Text="Добавление в историю начислений"></asp:Label></h4></center>
                            </div>
                            <div class="modal-body">
                                <div style="text-align: center">
                                    <asp:Label ID="Label_Calc_History_Add_Error1" runat="server" ForeColor="Red" Font-Size="Large" Font-Bold="true" Text=""></asp:Label><br />
                                    <asp:Label ID="Label_Calc_History_Add_Error2" runat="server" ForeColor="Red" Text=""></asp:Label><br />
                                    <br />
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="input-group">
                                            <asp:DropDownList ID="DropDownList_Calc_History_Add_Tar_ID" runat="server" class="form-control" Width="380" aria-describedby="sizing-addon_Calc_History_Add_Tar_ID" AutoPostBack="true"></asp:DropDownList>
                                            <span class="input-group-addon" id="sizing-addon_Calc_History_Add_Tar_ID">Тариф&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="input-group">
                                            <asp:DropDownList ID="DropDownList_Calc_History_Add_FT" runat="server" OnSelectedIndexChanged="DropDownList_Calc_History_Add_FT_SelectedIndexChanged" class="form-control" Width="380" aria-describedby="sizing-addon_Calc_History_Add_FT" AutoPostBack="true"></asp:DropDownList>
                                            <span class="input-group-addon" id="sizing-addon_Calc_History_Add_FT">Финансовый период&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="input-group">
                                            <asp:DropDownList ID="DropDownList_Calc_History_Add_Service" runat="server" class="form-control" Width="380" aria-describedby="sizing-addon_Calc_History_Add_Service" AutoPostBack="true"></asp:DropDownList>
                                            <span class="input-group-addon" id="sizing-addon_Calc_History_Add_Service">Услуга&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-4">
                                        <asp:TextBox ID="TextBox_Calc_History_Add_Norm_Coeff" class="form-control" runat="server" placeholder="Норм. коэфф." Width="170px"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_Calc_History_Add_Norm_Coeff" runat="server" TargetControlID="TextBox_Calc_History_Add_Norm_Coeff" FilterType="Custom" ValidChars="0123456789." />
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="TextBox_Calc_History_Add_Units" class="form-control" runat="server" placeholder="Единицы" Width="170px" MaxLength="15"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_Calc_History_Add_Units" runat="server" TargetControlID="TextBox_Calc_History_Add_Units" FilterType="Custom" ValidChars="0123456789." />
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="TextBox_Calc_History_Add_Cntr_Units" class="form-control" runat="server" placeholder="Ед. счетчика" Width="170px"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_Calc_History_Add_Cntr_Units" runat="server" TargetControlID="TextBox_Calc_History_Add_Cntr_Units" FilterType="Custom" ValidChars="0123456789." />
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-4">
                                        <asp:TextBox ID="TextBox_Calc_History_Add_Norm_Units" class="form-control" runat="server" placeholder="Ед. нормы" Width="170px"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_Calc_History_Add_Norm_Units" runat="server" TargetControlID="TextBox_Calc_History_Add_Norm_Units" FilterType="Custom" ValidChars="0123456789." />
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="TextBox_Calc_History_Add_Saldo" class="form-control" runat="server" placeholder="Сальдо" Width="170px" MaxLength="15"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_Calc_History_Add_Saldo" runat="server" TargetControlID="TextBox_Calc_History_Add_Saldo" FilterType="Custom" ValidChars="0123456789." />
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="TextBox_Calc_History_Add_Value" class="form-control" runat="server" placeholder="Значение" Width="170px"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_Calc_History_Add_Value" runat="server" TargetControlID="TextBox_Calc_History_Add_Value" FilterType="Custom" ValidChars="0123456789." />
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-4">
                                        <asp:TextBox ID="TextBox_Calc_History_Add_Added" class="form-control" runat="server" placeholder="Надбавка" Width="170px"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_Calc_History_Add_Added" runat="server" TargetControlID="TextBox_Calc_History_Add_Added" FilterType="Custom" ValidChars="0123456789." />
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="TextBox_Calc_History_Add_Paid" class="form-control" runat="server" placeholder="Платеж" Width="170px" MaxLength="15"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_Calc_History_Add_Paid" runat="server" TargetControlID="TextBox_Calc_History_Add_Paid" FilterType="Custom" ValidChars="0123456789." />
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="TextBox_Calc_History_Add_Penalties" class="form-control" runat="server" placeholder="Штраф" Width="170px"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_Calc_History_Add_Penalties" runat="server" TargetControlID="TextBox_Calc_History_Add_Penalties" FilterType="Custom" ValidChars="0123456789." />
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div style="text-align: center">
                                            <button id="Button_Calc_History_Add_Complete" runat="server" type="button" class="btn btn-default" style="color: #ff6800" onserverclick="Button_Calc_History_Add_Complete_Click" visible="true">
                                                <span aria-hidden="true" class="glyphicon glyphicon-plus-sign"></span>
                                                Добавить
                                            </button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
       
        <%-------------------------------------------------------------------- Модальное окно "Удалить из истории начислений" Delete_Calc_History---------------------------%>
        <div class="modal fade bd-example-modal-lg" id="myModal_Calc_History_delete" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <asp:UpdatePanel ID="UpdatePanel_Calc_History_delete" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-content" style="width: 550px">
                            <div class="modal-header" style="border-top-left-radius: 10px; color: #0033cc;">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                                <center><h4 class="modal-title"><asp:Label ID="Label_Calc_History_Dell_Header" runat="server" Text="Удаление из истории начислений"></asp:Label></h4></center>
                            </div>
                            <div class="modal-body" style="text-align: center;">
                                <div class="row" style="text-align: center; width: 100%; vertical-align: central; margin-left: 20px;">
                                    <asp:Label ID="Label_Calc_History_Delete_Error1" runat="server" ForeColor="Red" Font-Size="Large" Font-Bold="true" Text="!!!Внимание!!!"></asp:Label><br />
                                    <asp:Label ID="Label_Calc_History_Delete_Error2" runat="server" ForeColor="Red" Text="Вы уверены, что хотите удалить данную строку из истории начислений?"></asp:Label><br />
                                    <br />
                                    <button id="Button_Calc_History_Dell" runat="server" type="button" class=" btn btn-default" style="color: #ff6800" onserverclick="Button_Calc_History_Dell_Click" visible="true">
                                        <span aria-hidden="true" class="glyphicon glyphicon-remove"></span>
                                        Удалить
                                    </button>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    


        <%-------------------------------------------------------------------- Модальное окно "Добавить в новую вкладку" New_Add--------------------------------------------%>
        <div class="modal fade bd-example-modal-lg" id="myModal_New_Add" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <asp:UpdatePanel ID="UpdatePanel_New_Add" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-content" style="width: 640px">
                            <div class="modal-header" style="border-top-left-radius: 10px; color: #0033cc;">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                                <center><h4 class="modal-title"><asp:Label ID="Label_Header_New_Add" runat="server" Text="Добавление в новую вкладку"></asp:Label></h4></center>
                            </div>
                            <div class="modal-body">
                                <div style="text-align: center">
                                    <asp:Label ID="Label_New_Add_Error1" runat="server" ForeColor="Red" Font-Size="Large" Font-Bold="true" Text=""></asp:Label><br />
                                    <asp:Label ID="Label_New_Add_Error2" runat="server" ForeColor="Red" Text=""></asp:Label><br />
                                    <br />
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="input-group">
                                            <asp:DropDownList ID="DropDownList_New_Add_Service" runat="server" class="form-control" Width="380" aria-describedby="sizing-addon_New_Add_Service" AutoPostBack="true"></asp:DropDownList>
                                            <span class="input-group-addon" id="sizing-addon_New_Add_Service">Услуга&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-4">
                                        <asp:TextBox ID="TextBox_New_Add_Norm_Coeff" class="form-control" runat="server" placeholder="Норм. коэфф." Width="170px"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_New_Add_Norm_Coeff" runat="server" TargetControlID="TextBox_New_Add_Norm_Coeff" FilterType="Custom" ValidChars="0123456789." />
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="TextBox_New_Add_Units" class="form-control" runat="server" placeholder="Единицы" Width="170px" MaxLength="15"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_New_Add_Units" runat="server" TargetControlID="TextBox_New_Add_Units" FilterType="Custom" ValidChars="0123456789." />
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="TextBox_New_Add_Cntr_Units" class="form-control" runat="server" placeholder="Ед. счетчика" Width="170px"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_New_Add_Cntr_Units" runat="server" TargetControlID="TextBox_New_Add_Cntr_Units" FilterType="Custom" ValidChars="0123456789." />
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div style="text-align: center">
                                            <button id="Button_New_Add_Complete" runat="server" type="button" class="btn btn-default" style="color: #ff6800" visible="true" onserverclick="Button_New_Add_Complete_ServerClick">
                                                <span aria-hidden="true" class="glyphicon glyphicon-plus-sign"></span>
                                                Добавить
                                            </button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
             


    </div>

</asp:Content>
