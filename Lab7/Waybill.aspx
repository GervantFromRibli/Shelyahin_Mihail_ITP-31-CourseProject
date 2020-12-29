<%@ Page Title="Список накладных" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Waybill.aspx.cs" Inherits="Lab7.Waybill" %>
<asp:Content ID="ContentWaybill" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="LabelWaybill" runat="server" Font-Size="Large">Список накладных</asp:Label>
    <br />
    Найти накладную:&nbsp;Название поставщика:&nbsp;<asp:TextBox ID="ProviderNameField" runat="server" BorderStyle="Dotted" Width="15%" Text=""></asp:TextBox>
    Название мебели:&nbsp;<asp:DropDownList ID="DropDownListFurnit" runat="server" DataSourceID="SqlDataSourceFurniture" DataTextField="Name" DataValueField="Id" AutoPostBack="true">
            </asp:DropDownList>
    <asp:FormView ID="FormViewInsert" runat="server" DataKeyNames="Id" DataSourceID="SqlDataSourceWaybill">
        <InsertItemTemplate>
           <h4>Добавить:</h4>
            Номер поставщика:
            <asp:TextBox ID="ProvIdTextBox" runat="server" Text='<%# Bind("ProviderId") %>' />
            <asp:RangeValidator ID="ProvIdRegular" runat="server" ControlToValidate="ProvIdTextBox" ErrorMessage="Неправильное значение номера" MinimumValue="1" MaximumValue="1000" Type="Integer"></asp:RangeValidator>
            <br />
            Название поставщика:
            <asp:TextBox ID="ProvNameTextBox" runat="server" Text='<%# Bind("ProviderName") %>' />
            <asp:RegularExpressionValidator ControlToValidate="ProvNameTextBox" runat="server" ID="RegularProviderTextBox" ErrorMessage="Неправильная длина названия поставщика" ValidationExpression="^.{10,100}$"></asp:RegularExpressionValidator>
            <br />
            Дата поставки:
            <asp:TextBox ID="DateTextBox" runat="server" Text='<%# Bind("DateOfSupply") %>' />
            <asp:RangeValidator ID="RegularDate" runat="server" ControlToValidate="DateTextBox" ErrorMessage="Неправильное значение даты" MinimumValue="2000-01-01" MaximumValue="2022-01-01" Type="Date"></asp:RangeValidator>
            <br />
            Материал:
            <asp:TextBox ID="MaterialTextBox" runat="server" Text='<%# Bind("Material") %>' />
            <asp:RegularExpressionValidator ControlToValidate="MaterialTextBox" runat="server" ID="RegularMaterialTextBox" ErrorMessage="Неправильная длина материала" ValidationExpression="^.{10,60}$"></asp:RegularExpressionValidator>
            <br />
            Цена:
            <asp:TextBox ID="PriceTextBox" runat="server" Text='<%# Bind("Price") %>' />
            <asp:RangeValidator ID="RegularPrice" runat="server" ControlToValidate="PriceTextBox" ErrorMessage="Неправильное значение цены" MinimumValue="1" MaximumValue="1000" Type="Double"></asp:RangeValidator>
            <br />
            Вес:
            <asp:TextBox ID="WeightTextBox" runat="server" Text='<%# Bind("Weight") %>' />
            <asp:RangeValidator ID="RegularWeight" runat="server" ControlToValidate="WeightTextBox" ErrorMessage="Неправильное значение веса" MinimumValue="1" MaximumValue="1000" Type="Double"></asp:RangeValidator>
            <br />
            Название мебели:
            <asp:DropDownList ID="FurnitDropDownList" runat="server" DataSourceID="SqlDataSourceFurniture" DataTextField="Name" DataValueField="Id" SelectedValue='<%# Bind("FurnitureId") %>'>
            </asp:DropDownList>
            <br />
            ФИО работника:
            <asp:DropDownList ID="EmplDropDownList" runat="server" DataSourceID="SqlDataSourceEmployee" DataTextField="FIO" DataValueField="Id" SelectedValue='<%# Bind("EmployeeId") %>'>
            </asp:DropDownList>
            <br />
            <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Добавление" />
            &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Отмена" />
        </InsertItemTemplate>
        <ItemTemplate>
            <asp:LinkButton ID="NewButton" runat="server" CausesValidation="False" CommandName="New" Text="Добавить накладную" />
        </ItemTemplate>
    </asp:FormView>
    <br />

    <asp:Label ID="GridViewLabel" runat="server" Text="Накладные" Font-Bold="true"/>

    <asp:GridView ID="GridViewWaybill" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="SqlDataSourceWaybill">
        <Columns>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
            <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
            <asp:BoundField DataField="ProviderId" HeaderText="Номер поставщика" SortExpression="ProviderId" />
            <asp:BoundField DataField="ProviderName" HeaderText="Название поставщика" SortExpression="ProviderName" />
            <asp:BoundField DataField="DateOfSupply" HeaderText="Дата поставки" SortExpression="DateOfSupply" />
            <asp:BoundField DataField="Material" HeaderText="Материал" SortExpression="Material" />
            <asp:BoundField DataField="Price" HeaderText="Цена" SortExpression="Price" />
            <asp:BoundField DataField="Weight" HeaderText="Вес" SortExpression="Weight" />
            <asp:TemplateField HeaderText="Название мебели" SortExpression="Name">
                <EditItemTemplate>
                    <asp:DropDownList ID="FurnitureId" runat="server" DataSourceID="SqlDataSourceFurniture" DataTextField="Name" DataValueField="Id" SelectedValue='<%# Bind("FurnitureId") %>'>
                </asp:DropDownList>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="FurnitureID" runat="server"  Text='<%# Eval("Name") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="ФИО работника" SortExpression="FIO">
                <EditItemTemplate>
                    <asp:DropDownList ID="EmployeeId" runat="server" DataSourceID="SqlDataSourceEmployee" DataTextField="FIO" DataValueField="Id" SelectedValue='<%# Bind("EmployeeId") %>'>
                </asp:DropDownList>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="EmployeeID" runat="server"  Text='<%# Eval("FIO") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <asp:SqlDataSource ID="SqlDataSourceFurniture" runat="server" 
        ConnectionString="<%$ ConnectionStrings:FurnitConnection %>" 
        SelectCommand="SELECT [Id], [Name] FROM [Furniture]">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSourceEmployee" runat="server" 
        ConnectionString="<%$ ConnectionStrings:FurnitConnection %>" 
        SelectCommand="SELECT [Id], [FIO] FROM [Employees]">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSourceWaybill" runat="server" 
        ConnectionString="<%$ ConnectionStrings:FurnitConnection %>" 
        SelectCommand="SELECT Waybills.Id, Waybills.ProviderId, Waybills.ProviderName, Waybills.DateOfSupply, Waybills.Material, Waybills.Price, Waybills.Weight, Waybills.FurnitureId, Waybills.EmployeeId, Furniture.Name, Employees.FIO
        FROM [Waybills] INNER JOIN Furniture ON Waybills.FurnitureId = Furniture.Id INNER JOIN Employees ON Waybills.EmployeeId = Employees.Id
        WHERE (ProviderName LIKE '%'+ ISNULL(@ProviderName,'')+'%') AND (FurnitureId = @FurnitureId)" 
        DeleteCommand="DELETE FROM [Waybills] WHERE [Id] = @Id" 
        UpdateCommand="UPDATE [Waybills] SET [ProviderId] = @ProviderId, [ProviderName] = @ProviderName, [DateOfSupply] = @DateOfSupply, [Material] = @Material, [Price] = @Price, [Weight] = @Weight, [FurnitureId] = @FurnitureId, [EmployeeId] = @EmployeeId WHERE [Id] = @Id" 
        InsertCommand="INSERT INTO [Waybills] ([Id], [ProviderId], [ProviderName], [DateOfSupply], [Material], [Price], [Weight], [FurnitureId], [EmployeeId]) VALUES ((SELECT MAX(Waybills.Id) FROM Waybills) + 1, @ProviderId, @ProviderName, @DateOfSupply, @Material, @Price, @Weight, @FurnitureId, @EmployeeId)">
        <DeleteParameters>
            <asp:Parameter Name="Id" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="ProviderId" Type="Int32" />
            <asp:Parameter Name="ProviderName" Type="String" />
            <asp:Parameter Name="DateOfSupply" Type="DateTime" />
            <asp:Parameter Name="Material" Type="String" />
            <asp:Parameter Name="Price" Type="Decimal" />
            <asp:Parameter Name="Weight" Type="Double" />
            <asp:Parameter Name="FurnitureId" Type="Int32" />
            <asp:Parameter Name="EmployeeId" Type="Int32" />
        </InsertParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="ProviderNameField" Name="ProviderName" PropertyName="Text" DefaultValue="" ConvertEmptyStringToNull="False" DbType="String"/>
            <asp:ControlParameter ControlID="DropDownListFurnit" Name="FurnitureId" PropertyName="SelectedValue" DefaultValue="1" Type="Int32"/>
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="Id" Type="Int32" />
            <asp:Parameter Name="ProviderId" Type="Int32" />
            <asp:Parameter Name="ProviderName" Type="String" />
            <asp:Parameter Name="DateOfSupply" Type="DateTime" />
            <asp:Parameter Name="Material" Type="String" />
            <asp:Parameter Name="Price" Type="Decimal" />
            <asp:Parameter Name="Weight" Type="Double" />
            <asp:Parameter Name="EmployeeId" Type="Int32" />
            <asp:Parameter Name="FurnitureId" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>
</asp:Content>