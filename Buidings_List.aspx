<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/App.Master" CodeBehind="Buidings_List.aspx.vb" Inherits="App_Billing.Buidings_List" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    
    <br />
    <div style="border: 1px solid LightGray; background-color: #f8f6f6; border-radius: 3px; padding: 10px">
    <div class="alert-danger">
        <asp:Label ID="Label_danger" runat="server" Text=""></asp:Label>
    </div>
        

  
    
     <asp:DropDownList ID="DropDownList_UL" runat="server" AutoPostBack="True" ForeColor="Black" Font-Size="12px" AppendDataBoundItems="True" ToolTip="Выбор улицы" CssClass="dropdown-header">
                                                <asp:ListItem Value="0" Text="-Выбор улицы-"></asp:ListItem>
                                            </asp:DropDownList>


    </div>

    <br />
    <div style="border: 1px solid LightGray; background-color: #f8f6f6; border-radius: 3px; padding: 10px">
        <asp:GridView ID="GridViewBuildings" runat="server" DataSourceID="SqlDataSourceBuildings" AutoGenerateColumns="False" DataKeyNames="ID_build" EmptyDataText="Нет данных" Width="100%"
            RowStyle-BorderColor="#000099" HeaderStyle-BackColor="#f0f0f0" ForeColor="#666666"  CssClass="table-condensed">
            <Columns>
       
                 <asp:TemplateField ShowHeader="False">
                     <ItemTemplate>
                        <asp:ImageButton ID="ImageButtonChoice" runat="server" CommandName="Select" CausesValidation="False" ImageUrl="~/Images/Choice_16_16.png" Text="Список квартир" ToolTip="Список квартир" Width="18px"   />
                    </ItemTemplate>
                     </asp:TemplateField>
       
                <asp:BoundField DataField="ID_build" HeaderText="Код дома" ReadOnly="True" SortExpression="ID_build" Visible="True" />
                <asp:TemplateField HeaderText="Номер дома" SortExpression="Build">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBoxBuild" runat="server" Width="70px" BackColor="#FFFFCC"  Text='<%# Bind("Build") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="LabelBuild" runat="server"  Width="70px" Text='<%# Bind("Build") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                     <%--<asp:TemplateField HeaderText="Кол-во <br/> подъездов">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBoxEntrances" runat="server"   Width="70px" BackColor="#FFFFCC"  Text='<%# Bind("Entrances") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="LabelEntrances" runat="server"  Width="70px" Text='<%# Bind("Entrances") %>'></asp:Label>
                                </ItemTemplate>
                 </asp:TemplateField>--%>
                 <asp:TemplateField HeaderText="Кол-во <br/> этажей">
                     <EditItemTemplate>
                         <asp:TextBox ID="TextBoxFloors" runat="server"   Width="70px" BackColor="#FFFFCC"  Text='<%# Bind("Floors") %>'></asp:TextBox>
                     </EditItemTemplate>
                     <ItemTemplate>
                         <asp:Label ID="LabelFloors" runat="server"  Width="70px" Text='<%# Bind("Floors") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>

               <%-- ***************************************************INDEX*********************************************************--%>

                            <asp:TemplateField HeaderText="Почтовый <br/> индекс" SortExpression="Index_id">
                <EditItemTemplate>
                    <asp:DropDownList ID="DropDownListIndex" runat="server"  Width="100%" AutoPostBack="True" DataSourceID="SqlDataSourceIndex" DataTextField="Index_number" DataValueField="ID_Index" SelectedValue='<%# Bind("Index_id")%>' BackColor="#FFFFCC" ToolTip="Почтовый индекс">
                        </asp:DropDownList>

                </EditItemTemplate>
                <ItemTemplate>
                         
                     <asp:Label ID="LabelIndex" runat="server"  Width="70px" Text='<%# Bind("Index_number")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
               <%-- -----------------------------------------------------------------------------------------------------------------------------------%>
                      <%-- ***************************************************TECH*********************************************************--%>
                   <asp:TemplateField HeaderText="Тех. участок" SortExpression="Index_id">
                <EditItemTemplate>
                    <asp:DropDownList ID="DropDownListTech" runat="server"  Width="100%" AutoPostBack="True" DataSourceID="SqlDataSourceTech" DataTextField="Name" DataValueField="ID_Tech" SelectedValue='<%# Bind("ID_tech")%>' BackColor="#FFFFCC" ToolTip="Тех. участок">
                        </asp:DropDownList>

                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="LabelTech" runat="server" Text='<%# Bind("Tech_name")%>' ToolTip="Тех. участок"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
               <%-- -----------------------------------------------------------------------------------------------------------------------------------%>

                <asp:TemplateField HeaderText="ФИАС" SortExpression="FIAS_GUD">
                    <EditItemTemplate>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage='Не верный формат ФИАС' ControlToValidate="TextBox2" ForeColor="Red"
                            ValidationExpression="^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$" 
                            SetFocusOnError="True" Visible="True" Display="Dynamic"></asp:RegularExpressionValidator>
                        <asp:TextBox ID="TextBox2" runat="server" BackColor="#FFFFCC" Width="100%" Text='<%# Bind("FIAS_GUD") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("FIAS_GUD") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                   
                    <EditItemTemplate>
                        <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="True" CommandName="Update" ImageUrl="~/Images/save.png" Text="Сохранить" ToolTip="Сохранить" Width="18px" ImageAlign="Left" />
        
                        <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" CommandName="Cancel" ImageUrl="~/Images/cancel.png" Text="Отмена" ToolTip="Отмена" Width="18px"  ImageAlign="Right" />
                   </EditItemTemplate>
                     <ItemTemplate>
                        <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Edit" ImageUrl="~/Images/edit.png" Text="Изменить" ToolTip="Изменить" Width="18px"   />
                    </ItemTemplate>
                </asp:TemplateField>
   
                
   
            </Columns>

<HeaderStyle BackColor="#F0F0F0"></HeaderStyle>

<RowStyle BorderColor="#000099"></RowStyle>
            <SelectedRowStyle BorderColor="#339933" BorderWidth="2px" CssClass="active" Font-Bold="true" />
         </asp:GridView>


         <asp:SqlDataSource ID="SqlDataSourceBuildings" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="APP_Buildings" SelectCommandType="StoredProcedure" >
             <SelectParameters>
                 <asp:ControlParameter ControlID="DropDownList_UL" DefaultValue="0" Name="street_id" PropertyName="SelectedValue" Type="Int32" />             
             </SelectParameters>
         </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSourceIndex" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [ID_Index], [Index_number] FROM [Index_number]"></asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSourceTech" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT ID_Tech, Name FROM Tech"></asp:SqlDataSource>
         </div>

     
  
     <%--**********************************************Modal_Flats********************************************************--%>
    <div class="modal animated slideInDown" id="Modal_Flats" role="document" aria-labelledby="myModalLabel" aria-hidden="true" style="height:100%;overflow-y:auto">
        <div class="modal-dialog">
            <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional" >
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header" style="border-top-left-radius: 10px; color: #000099;text-align:right;">
                            <asp:ImageButton ID="ButtonClosed"  runat="server"  ImageUrl="~/Images/close_box.png" Width="16px"   />
                                   <center><h4><asp:Label ID="lblModalTitle" runat="server" Text=""></asp:Label>                           
                                </h4></center>
                              <table class="table-condensed" style="width:100%">
                                <tr>
                                    <td>
                                        <asp:TextBox ID="TextBoxFlat1" class="form-control" placeholder="С квартиры" runat="server"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtenderTextBoxFlat1" runat="server" TargetControlID="TextBoxFlat1" FilterType="Numbers" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBoxFlat2" class="form-control" placeholder="по квартиру" runat="server"></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtenderTextBoxFlat2" runat="server" TargetControlID="TextBoxFlat2" FilterType="Numbers" />
                                    </td>
                                    <td>
                                        <button id="ButtonAddOcc" runat="server" type="button" class="btn btn-default" style="color: #ff6800" onserverclick="ButtonAddOcc_ServerClick">
                                            <span aria-hidden="true" class="glyphicon glyphicon-plus-sign"></span>
                                            Добавить
                                        </button>

                                        

                                    </td>
                                </tr>
                            </table>
                           

                            </div>
                        
                         




                        <div class="modal-body"  >
                            <asp:GridView ID="GridViewFlats" runat="server" AutoGenerateColumns="false" DataKeyNames="ID_flats"  DataSourceID="SqlDataSourceFlats" EmptyDataText="Нет данных" Width="100%"
            RowStyle-BorderColor="#000099" HeaderStyle-BackColor="#f0f0f0" ForeColor="#666666"  CssClass="table-condensed"   AllowPaging="True" PageSize="100" >
                
                                
                                <Columns>
                                      <asp:BoundField DataField="ID_flats" HeaderText="ID_flats" ReadOnly="True" SortExpression="ID_flats" Visible="false" />
                                     <asp:TemplateField ShowHeader="False" ControlStyle-Height="16px">
                     <ItemTemplate>
                        <asp:ImageButton ID="ImageButtonDelete" runat="server" CommandName="Delete" CausesValidation="False" ImageUrl="~/Images/delete.png" Text="Удалить" ToolTip="Удалить" Width="16px"   OnClick="ImageButton1_Click" OnClientClick="return confirm('Вы действительно хотите удалить квартиру?');"/>
                    </ItemTemplate>
                     </asp:TemplateField>


                                   <%-- ******************************************************************************************************************************--%>
                                    <asp:TemplateField HeaderText="Номер квартиры" SortExpression="Flat">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBoxFlat" runat="server" Width="70px" BackColor="#FFFFCC" Text='<%# Bind("Flat")%>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="LabelFlat" runat="server" Width="100%" Text='<%# Bind("Flat")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--________________________________________________________________________________________________________________________________________--%>

                                     <%-- ******************************************************************************************************************************--%>
                                    <asp:TemplateField HeaderText="Номер подъезда" SortExpression="Entrance">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBoxEntrance" runat="server" Width="70px" BackColor="#FFFFCC" Text='<%# Bind("Entrance")%>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="LabelEntrance" runat="server" Width="100%" Text='<%# Bind("Entrance")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--________________________________________________________________________________________________________________________________________--%>
                                    <%-- ******************************************************************************************************************************--%>
                                    <asp:TemplateField HeaderText="Этаж" SortExpression="FloorFlat">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBoxFloorFlat" runat="server" Width="70px" BackColor="#FFFFCC" Text='<%# Bind("FloorFlat")%>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="LabelFloorFlat" runat="server" Width="100%" Text='<%# Bind("FloorFlat")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--________________________________________________________________________________________________________________________________________--%>
                                    <asp:TemplateField ShowHeader="False" ControlStyle-Height="16px">

                                        <EditItemTemplate>
                                            <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="True" CommandName="Update" ImageUrl="~/Images/save.png" Text="Сохранить" ToolTip="Сохранить" Width="16px" OnClick="ImageButton1_Click" ImageAlign="Left" />

                                            <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" CommandName="Cancel" ImageUrl="~/Images/cancel.png" Text="Отмена" ToolTip="Отмена" Width="16px" OnClick="ImageButton1_Click" ImageAlign="Right" />
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Edit" ImageUrl="~/Images/edit.png" Text="Изменить" ToolTip="Изменить" Width="16px"  OnClick="ImageButton1_Click"/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                
                            </asp:GridView>

                            <asp:SqlDataSource ID="SqlDataSourceFlats" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="APP_Flats" SelectCommandType="StoredProcedure">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="GridViewBuildings" DefaultValue="0" Name="id_builds" PropertyName="SelectedValue" Type="Int32" />                           
                                </SelectParameters>
                            </asp:SqlDataSource>
                   
                                  
                      </div>

                        <div class="modal-footer">
                         <div class="alert-success">
                                <asp:Label ID="LabelChangeSucces" runat="server" Text="" ></asp:Label>
                            </div>
                            
                    </div>
                  </div>
                    
                           
                </ContentTemplate>
                
            </asp:UpdatePanel>
        </div>
    </div>
 <div class="modal  animated zoomInRight" id="modal_error" tabindex="-1" role="dialog" aria-labelledby="modalLabel2"  style="overflow-y:hidden">
  <div class="modal-dialog"  role="dialog">
             <asp:UpdatePanel ID="UpdatePanelError" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional" >
                 <ContentTemplate>
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-label="Закрыть">
          <span aria-hidden="true">×</span>
        </button>
      <centr>  <h4 class="modal-title" style="color:red;text-align:center" id="modalLabel1">Ошибка редактирования</h4> 
      </div>
        <div class="modal-body">
  <h5>   <div class="alert-danger" style="text-align:center">
                <asp:Label ID="LabelChangeError" runat="server" Text=""></asp:Label>
            </div></h5> 
        </div>
    </div>
                     </ContentTemplate>
                 </asp:UpdatePanel>
  </div>
 </div>
   
</asp:Content>
