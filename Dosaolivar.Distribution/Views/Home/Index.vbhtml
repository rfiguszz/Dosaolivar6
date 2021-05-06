@Code
    ViewData("Title") = "Dosaolivar para iOS"
End Code

<div class="row">
    <div class="col-md-6 col-md-offset-3 text-center">
        <img src="~/Images/logo_splash.png" width="200" />
        <h2>para iOS</h2>
        <p>Pulsa el siguiente botón para actualizar o instalar la app en tu dispositivo iOS:</p>
        <a class="btn btn-primary btn-lg" id="settings" href="itms-services://?action=download-manifest&url=https://dosaolivar.azurewebsites.net/UserFiles/dosaolivar.plist">Instalar</a>
        <p style="margin-top: 10px; font-size: 85%; color: red">Versión de desarrollo</p>
        <p>(Nota: requiere iOS 8.0 o superior y 50Mb de espacio libre en tu dispositivo)</p>
        <a href="https://support.apple.com/en-us/HT204460">Guía para la instalación</a>
    </div>
</div>