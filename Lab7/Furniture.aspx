<%@ Page Title="Список мебели" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Furniture.aspx.cs" Inherits="Lab7.Furniture" %>
<asp:Content ID="ContentFurniture" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="LabelFurnit" runat="server" Font-Size="Large">Список мебели</asp:Label>
    <br />
    Найти мебель:&nbsp;<asp:TextBox ID="NameField" runat="server" BorderStyle="Dotted" Width="15%" Text=""></asp:TextBox>
    <asp:FormView ID="FormViewInsert" runat="server" DataKeyNames="Id" DataSourceID="SqlDataSourceFurniture">
        <InsertItemTemplate>
           <h4>Добавить:</h4>
            Название:
            <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>' />
            <asp:RegularExpressionValidator ControlToValidate="NameTextBox" runat="server" ID="RegularNameTextBox" ErrorMessage="Неправильная длина названия" ValidationExpression="^.{10,50}$"></asp:RegularExpressionValidator>
            <br />
            Описание:
            <asp:TextBox ID="DescrTextBox" runat="server" Text='<%# Bind("Description") %>' />
            <asp:RegularExpressionValidator ControlToValidate="DescrTextBox" runat="server" ID="RegularDescriptionTextBox" ErrorMessage="Неправильная длина описания" ValidationExpression="^.{20,200}$"></asp:RegularExpressionValidator>
            <br />
            Материал:
            <asp:TextBox ID="MaterialTextBox" runat="server" Text='<%# Bind("Material") %>' />
            <asp:RegularExpressionValidator ControlToValidate="MaterialTextBox" runat="server" ID="RegularMaterialTextBox" ErrorMessage="Неправильная длина материала" ValidationExpression="^.{10,60}$"></asp:RegularExpressionValidator>
            <br />
            Цена:
            <asp:TextBox ID="PriceTextBox" runat="server" Text='<%# Bind("Price") %>' />
            <asp:RangeValidator ID="PriceRegular" runat="server" ControlToValidate="PriceTextBox" ErrorMessage="Неправильное значение цены" MinimumValue="1" MaximumValue="1000" Type="Double"></asp:RangeValidator>
            <br />
            Количество:
            <asp:TextBox ID="CountTextBox" runat="server" Text='<%# Bind("Count") %>' />
            <asp:RangeValidator ID="CountRegular" runat="server" ControlToValidate="CountTextBox" ErrorMessage="Неправильное значение кол-ва" MinimumValue="1" MaximumValue="1000" Type="Integer"></asp:RangeValidator>
            <br />
            <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Добавление" />
            &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Отмена" />
        </InsertItemTemplate>
        <ItemTemplate>
            <asp:LinkButton ID="NewButton" runat="server" CausesValidation="False" CommandName="New" Text="Добавить мебель" />
        </ItemTemplate>
    </asp:FormView>
    <br />

    <asp:Label ID="GridViewLabel" runat="server" Text="Мебель" Font-Bold="true"></asp:Label>

<asp:GridView ID="GridViewFurniture" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="SqlDataSourceFurniture">
    <Columns>
        <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
        <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
        <asp:BoundField DataField="Name" HeaderText="Название" SortExpression="Name" />
        <asp:BoundField DataField="Description" HeaderText="Описание" SortExpression="Description" />
        <asp:BoundField DataField="Material" HeaderText="Материал" SortExpression="Material" />
        <asp:BoundField DataField="Price" HeaderText="Цена" SortExpression="Price" />
        <asp:BoundField DataField="Count" HeaderText="Количество" SortExpression="Count" />
    </Columns>
</asp:GridView>
<asp:SqlDataSource ID="SqlDataSourceFurniture" runat="server" 
    ConnectionString="<%$ ConnectionStrings:FurnitConnection %>" 
    SelectCommand="SELECT * FROM [Furniture] WHERE ((Name LIKE '%'+ ISNULL(@Name,'')+'%'))" 
    DeleteCommand="DELETE FROM [Furniture] WHERE [Id] = @Id" 
    UpdateCommand="UPDATE [Furniture] SET [Name] = @Name, [Description] = @Description, [Material] = @Material, [Price] = @Price, [Count] = @Count WHERE [Id] = @Id" 
    InsertCommand="INSERT INTO [Furniture] ([Id], [Name], [Description], [Material], [Price], [Count]) VALUES ((SELECT MAX(Furniture.Id) FROM Furniture) + 1, @Name, @Description, @Material, @Price, @Count)">
    <DeleteParameters>
        <asp:Parameter Name="Id" Type="Int32" />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="Name" Type="String" />
        <asp:Parameter Name="Description" Type="String" />
        <asp:Parameter Name="Material" Type="String" />
        <asp:Parameter Name="Price" Type="Decimal" />
        <asp:Parameter Name="Count" Type="Int32" />
    </InsertParameters>
    <SelectParameters>
        <asp:ControlParameter ControlID="NameField"  Name="Name" PropertyName="Text" DefaultValue="" ConvertEmptyStringToNull="False" DbType="String"/>
    </SelectParameters>
    <UpdateParameters>
        <asp:Parameter Name="Id" Type="Int32" />
        <asp:Parameter Name="Name" Type="String" />
        <asp:Parameter Name="Description" Type="String" />
        <asp:Parameter Name="Material" Type="String" />
        <asp:Parameter Name="Price" Type="Decimal" />
        <asp:Parameter Name="Count" Type="Int32" />
    </UpdateParameters>
</asp:SqlDataSource>

</asp:Content>