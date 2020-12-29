<%@ Page Title="Список работников" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Employee.aspx.cs" Inherits="Lab7.Employee" %>
<asp:Content ID="ContentEmployee" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="LabelEmpl" runat="server" Font-Size="Large">Список работников</asp:Label>
    <br />
    Найти работника:&nbsp;<asp:TextBox ID="FIOField" runat="server" BorderStyle="Dotted" Width="15%" Text=""></asp:TextBox>
    <asp:FormView ID="FormViewInsert" runat="server" DataKeyNames="Id" DataSourceID="SqlDataSourceEmployees">
        <InsertItemTemplate>
           <h4>Добавить:</h4>
            ФИО:
            <asp:TextBox ID="FIOTextBox" runat="server" Text='<%# Bind("FIO") %>' />
            <asp:RegularExpressionValidator ControlToValidate="FIOTextBox" runat="server" ID="RegularFIOTextBox" ErrorMessage="Неправильная длина ФИО" ValidationExpression="^.{18,100}$"></asp:RegularExpressionValidator>
            <br />
            Должность:
            <asp:TextBox ID="PositionTextBox" runat="server" Text='<%# Bind("Position") %>' />
            <asp:RegularExpressionValidator ControlToValidate="PositionTextBox" runat="server" ID="RegularPositionTextBox" ErrorMessage="Неправильная длина должности" ValidationExpression="^.{10,50}$"></asp:RegularExpressionValidator>
            <br />
            Образование:
            <asp:TextBox ID="EducationTextBox" runat="server" Text='<%# Bind("Education") %>' />
            <asp:RegularExpressionValidator ControlToValidate="EducationTextBox" runat="server" ID="RegularEducationTextBox" ErrorMessage="Неправильная длина образования" ValidationExpression="^.{10,200}$"></asp:RegularExpressionValidator>
            <br />
            <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Добавление" />
            &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Отмена" />
        </InsertItemTemplate>
        <ItemTemplate>
            <asp:LinkButton ID="NewButton" runat="server" CausesValidation="False" CommandName="New" Text="Добавить работника" />
        </ItemTemplate>
    </asp:FormView>
                <br />

    <asp:Label ID="GridViewLabel" runat="server" Text="Работники" Font-Bold="true"></asp:Label>

<asp:GridView ID="GridViewEmployee" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="SqlDataSourceEmployees">
    <Columns>
        <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
        <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
        <asp:BoundField DataField="FIO" HeaderText="ФИО" SortExpression="FIO" />
        <asp:BoundField DataField="Position" HeaderText="Должность" SortExpression="Position" />
        <asp:BoundField DataField="Education" HeaderText="Образование" SortExpression="Education" />
    </Columns>
</asp:GridView>
<asp:SqlDataSource ID="SqlDataSourceEmployees" runat="server" 
    ConnectionString="<%$ ConnectionStrings:FurnitConnection %>" 
    SelectCommand="SELECT * FROM [Employees] WHERE ((FIO LIKE '%'+ ISNULL(@FIO,'')+'%'))" 
    DeleteCommand="DELETE FROM [Employees] WHERE [Id] = @Id" 
    UpdateCommand="UPDATE [Employees] SET [FIO] = @FIO, [Position] = @Position, [Education] = @Education WHERE [Id] = @Id" 
    InsertCommand="INSERT INTO [Employees] ([Id], [FIO], [Position], [Education]) VALUES ((SELECT MAX(Employees.Id) FROM Employees) + 1, @FIO, @Position, @Education)">
    <DeleteParameters>
        <asp:Parameter Name="Id" Type="Int32" />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="FIO" Type="String" />
        <asp:Parameter Name="Position" Type="String" />
        <asp:Parameter Name="Education" Type="String" />
    </InsertParameters>
    <SelectParameters>
        <asp:ControlParameter ControlID="FIOField"  Name="FIO" PropertyName="Text" DefaultValue="" ConvertEmptyStringToNull="False" DbType="String"/>
    </SelectParameters>
    <UpdateParameters>
        <asp:Parameter Name="Id" Type="Int32" />
        <asp:Parameter Name="FIO" Type="String" />
        <asp:Parameter Name="Position" Type="String" />
        <asp:Parameter Name="Education" Type="String" />
    </UpdateParameters>
</asp:SqlDataSource>

</asp:Content>
