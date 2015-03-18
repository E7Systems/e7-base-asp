<%@ Page Title="Projects" Language="C#" MasterPageFile="~/RsffMasterPage.master" AutoEventWireup="true" CodeFile="Projects.aspx.cs" Inherits="Projects" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    I am your projects grid title
    <br />
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
    <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
    <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
    <br />
    <asp:GridView ID="ProjectsGridView" runat="server" AutoGenerateColumns="False" 
        DataSourceID="ProjectsGridViewObjectDataSource" AllowPaging="True" AllowSorting="True">
        <Columns>
            <asp:BoundField DataField="Address" HeaderText="Address" 
                SortExpression="Address" />
            <asp:BoundField DataField="APN" HeaderText="APN" SortExpression="APN" />
            <asp:BoundField DataField="ProjectName" HeaderText="Project Name" 
                SortExpression="ProjectName" />
            <asp:BoundField DataField="PlanCheckNumber" HeaderText="Plan Check Number" 
                SortExpression="PlanCheckNumber" />
            <asp:BoundField DataField="Notes" HeaderText="Notes" SortExpression="Notes" />
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="ProjectsGridViewObjectDataSource" runat="server" 
        SelectMethod="GetAllProjectsDataTable" 
        TypeName="Rsff.BusinessLayer.ProjectsBusinessLogic">
    </asp:ObjectDataSource>
</asp:Content>

