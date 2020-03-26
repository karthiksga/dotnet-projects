
Partial Class PageA
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetExpires(DateTime.Now)
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
    End Sub
End Class
