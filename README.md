# mosaic-sprites-jump


<details>
  <summary><h1>Español </h1></summary>
  
  Este juego que no es juego trata de mostrar los sprites de un portfolio o tienda de sprites con un minijuego sin tener conocimientos de programación ni de ningún motor de videojuegos.
  
  Este es un proyecto de código abierto, siéntete libre de corregir y hacer tu propia versión.
  
  
  El funcionamiento es simple.<br>
  Cambias las imágenes del directorio /StreamingAssets/default/ a tu antojo.<br>
  Puedes cambiar la resolución de los sprites, pero, es recomendable intentar tener la misma relación aspecto.<br>
  
  Si quieres tener más de uno puedes crear directorios iguales a default pero cambiando el nombre del directorio.<br>
  Para acceder a estos tendrás que usar urlDeTuWeb#Nombre-Directorio.
  <br>
  Está pensado para que se incruste en una página web.




<h3>Configuración del proyecto</h3>

<table border="1">

<tr>
  <th>Nombre</th>
  <th>Valor</th>
  <th>Descripción</th>
</tr>

<tr>
  <th>isLoaded</th>
  <td>true</td>
  <td>Siempre en true; puede romper el proyecto.</td>
</tr>
<tr>
  <th>isPixelArt</th>
  <td>true (si) o false (no)</td>
  <td>Determina si se va a usar pixelart; si un sprite es menor a 128 píxeles de alto o largo, se pone automáticamente en modo pixelart.</td>
</tr>
<tr>
  <th>xPlayerMove</th>
  <td>Número decimal</td>
  <td>Si los sprites del jugador no están en el centro, se puede solucionar cambiando este valor. Si está a la izquierda con respecto al centro, el jugador deberá estar en números negativos; si no, en positivos.</td>
</tr>
<tr>
  <th>xPlayerDead</th>
  <td>Número decimal</td>
  <td>Cuando el jugador muere, si debe desplazarse en el eje x, es decir, en horizontal.</td>
</tr>
<tr>
  <th>yPlayerDead</th>
  <td>Número decimal</td>
  <td>Cuando el jugador muere, si debe desplazarse en el eje y, es decir, en vertical.</td>
</tr>
<tr>
  <th>maxFPS</th>
  <td>Número entero</td>
  <td>Solo disponible en la versión de Windows. Son los frames por segundo a los que estará limitado; esto evita que se usen la mayor cantidad de recursos. Si aún así quiere deslimitarlo, puede poner "-1".</td>
</tr>
<tr>
  <th>framesWalk</th>
  <td>Número entero</td>
  <td>Número de sprites en la animación de caminar.</td>
</tr>
<tr>
  <th>framesRun</th>
  <td>Número entero</td>
  <td>Número de sprites en la animación de correr.</td>
</tr>
<tr>
  <th>framesIdle</th>
  <td>Número entero</td>
  <td>Número de sprites en la animación de inactivo.</td>
</tr>
<tr>
  <th>framesJump</th>
  <td>Número entero</td>
  <td>Número de sprites en la animación de salto.</td>
</tr>
<tr>
  <th>framesDead</th>
  <td>Número entero</td>
  <td>Número de sprites en la animación de morir.</td>
</tr>
<tr>
  <th>delayFramesInSeconds</th>
  <td>Segundos</td>
  <td>Tiempo en segundos entre cada imagen en una animación; se pueden usar decimales.</td>
</tr>
<tr>
  <th>timeStartRun</th>
  <td>Segundos</td>
  <td>Tiempo en el que tarda el jugador en cambiar de moverse a correr.</td>
</tr>

</table>




</ul>
<br>
<a href="https://github.com/AndresGraneroSala/mosaic-sprites-jump/tree/main/Builds/Web">Pantilla en web para descargar </a> <br>
Ejemplo en web para jugar alojado en firebase

<ul> 
  

<li> <a href="https://test-url-e89fe.web.app/">Default</a> </li>
<li> <a href="https://test-url-e89fe.web.app/#girl">Chica</a> </li>

</ul>

</details>


<details>
  <summary><h1>English </h1></summary>

This non-game game is about showcasing sprites from a portfolio or sprite shop with a mini-game, without requiring programming knowledge or any game engine.

This is an open-source project, feel free to correct and create your own version.

The operation is simple.<br>
You can change the images in the /StreamingAssets/default/ directory as you wish.<br>
You can change the resolution of the sprites, but it is recommended to try to maintain the same aspect ratio.<br>

If you want to have more than one, you can create directories identical to default but changing the directory name.<br>
To access these, you will need to use yourWebURL#Directory-Name.<br>
It is intended to be embedded in a web page.


<table border="1">

<tr>
  <th>Name</th>
  <th>Value</th>
  <th>Description</th>
</tr>

<tr>
  <th>isLoaded</th>
  <td>true</td>
  <td>Always true; can break the project.</td>
</tr>
<tr>
  <th>isPixelArt</th>
  <td>true (yes) or false (no)</td>
  <td>Determines whether pixel art will be used; if a sprite is less than 128 pixels in height or width, it is automatically set to pixel art mode.</td>
</tr>
<tr>
  <th>xPlayerMove</th>
  <td>Decimal number</td>
  <td>If the player's sprites are not centered, this value can be adjusted. If it is to the left of the center, the player should be in negative numbers; otherwise, in positive numbers.</td>
</tr>
<tr>
  <th>xPlayerDead</th>
  <td>Decimal number</td>
  <td>When the player dies, if they should move on the x-axis, i.e., horizontally.</td>
</tr>
<tr>
  <th>yPlayerDead</th>
  <td>Decimal number</td>
  <td>When the player dies, if they should move on the y-axis, i.e., vertically.</td>
</tr>
<tr>
  <th>maxFPS</th>
  <td>Integer</td>
  <td>Available only in the Windows version. Specifies the frames per second to be limited; this prevents the excessive use of resources. If you still want to remove the limit, you can set it to "-1".</td>
</tr>
<tr>
  <th>framesWalk</th>
  <td>Integer</td>
  <td>Number of sprites in the walking animation.</td>
</tr>
<tr>
  <th>framesRun</th>
  <td>Integer</td>
  <td>Number of sprites in the running animation.</td>
</tr>
<tr>
  <th>framesIdle</th>
  <td>Integer</td>
  <td>Number of sprites in the idle animation.</td>
</tr>
<tr>
  <th>framesJump</th>
  <td>Integer</td>
  <td>Number of sprites in the jumping animation.</td>
</tr>
<tr>
  <th>framesDead</th>
  <td>Integer</td>
  <td>Number of sprites in the death animation.</td>
</tr>
<tr>
  <th>delayFramesInSeconds</th>
  <td>Seconds</td>
  <td>Time in seconds between each image in an animation; decimal values can be used.</td>
</tr>
<tr>
  <th>timeStartRun</th>
  <td>Seconds</td>
  <td>Time it takes for the player to transition from walking to running.</td>
</tr>

</table>


<br>
<a href="https://github.com/AndresGraneroSala/mosaic-sprites-jump/tree/main/Builds/Web">Web template for downloading </a> <br>
Example on the web to play hosted on Firebase.
<ul>   

<li> <a href="https://test-url-e89fe.web.app/">Default</a> </li>
<li> <a href="https://test-url-e89fe.web.app/#girl">Girl</a> </li>

</ul>


</details>

