namespace ApiMails.common
{
    public class BodyMail
    {
        public string body { get; set; }

        public BodyMail()
        {
            body = " <div class='row flex-lg-row-reverse align-items-center g-5 py-5'><div class='col-10 col-sm-8 col-lg-6'><img class='img-fluid d-block mx-lg-auto' src='[urlQr]' width='400' height='200' loading='lazy'></div><div class='col-lg-6'><h1 class='display-5 fw-bold lh-1 mb-3'>Se ha enviado el QR</h1><p class='lead'>Archivo: [archivo] </p><div class='d-grid gap-2 d-md-flex justify-content-md-start'></div></div></div>";
        }
    }
}
