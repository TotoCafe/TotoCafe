using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

        authCookie.Expires = DateTime.Now.AddDays(-1);

        Response.Cookies.Add(authCookie);
        Response.Redirect("Index.aspx");
        /* sohos@gmail.com olarak yarattığımız ilk company'nin sign up ve login kısmında
           ismi split('@') yapılarak "sohos" kısmı şifrelenmiş bir şekilde cookie'ye eklendi.
           Cookie'den bu ismi almak istediğimizde ticket.Name metoduyla 'sohos' ismine ulaşıyoruz.
           Her company'nin kendi ismi böylece cookie tarafından taşınmış olacak.
         
           Kullanılacak kısım: Veritabanında tablo isimleri --> 
           example: 'companyName'_Category şeklinde kaydedildi.
           Sorgular yazılırken company ismine ihtiyacımız olduğu için dinamik işlem yaptığımızdan dolayı
           cookie'deki kayıtlı adı kullanağız.
         */

       
    }
}
