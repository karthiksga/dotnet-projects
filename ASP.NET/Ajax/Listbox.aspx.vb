
Partial Class Listbox
    Inherits System.Web.UI.Page

    Protected Sub lstboxRight_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstboxRight.SelectedIndexChanged
        Response.Write(lstboxRight.Items.Count)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnRight_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRight.Click
        Dim objListItem As New ListItem
        objListItem.Text = txtHiddenText.Value
        objListItem.Value = txtHiddenValue.Value
        lstboxRight.Items.Add(objListItem)
    End Sub
End Class
