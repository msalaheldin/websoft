
function selectedData() {
    var selectedItem = document.getElementById("municipalities").value;
    
    fetch('data/'+selectedItem+'.json')
    .then((response) => {
        return response.json();
    })
    .then((myJson) => {
        console.log(myJson.Skolenheter);

        if(myJson.Skolenheter.length>0){
            var temp ="";
            myJson.Skolenheter.forEach((u) => {
                temp+="<tr>";
                temp += "<td>"+u.Kommunkod+"</td>"
                temp += "<td>"+u.PeOrgNr+"</td>"
                temp += "<td>"+u.Skolenhetskod+"</td>"
                temp += "<td>"+u.Skolenhetsnamn+"</td></tr>";

            });
    
        document.getElementById('data').innerHTML = temp;
    }
            });
        
}

function hideTheDuck(){
    var duck = document.getElementById('duck');
    if(duck.style.display=="inline"){
        duck.style.display="none";
    }else
    {
       duck.style.display="inline";
    }
    
}

dragElement(document.getElementById("mydiv"));

function dragElement(elmnt) {
  var pos1 = 0, pos2 = 0, pos3 = 0, pos4 = 0;
  if (document.getElementById(elmnt.id + "header")) {
    // if present, the header is where you move the DIV from:
    document.getElementById(elmnt.id + "header").onmousedown = dragMouseDown;
  } else {
    // otherwise, move the DIV from anywhere inside the DIV:
    elmnt.onmousedown = dragMouseDown;
  }

  function dragMouseDown(e) {
    e = e || window.event;
    e.preventDefault();
    // get the mouse cursor position at startup:
    pos3 = e.clientX;
    pos4 = e.clientY;
    document.onmouseup = closeDragElement;
    // call a function whenever the cursor moves:
    document.onmousemove = elementDrag;
  }

  function elementDrag(e) {
    e = e || window.event;
    e.preventDefault();
    // calculate the new cursor position:
    pos1 = pos3 - e.clientX;
    pos2 = pos4 - e.clientY;
    pos3 = e.clientX;
    pos4 = e.clientY;
    // set the element's new position:
    elmnt.style.top = (elmnt.offsetTop - pos2) + "px";
    elmnt.style.left = (elmnt.offsetLeft - pos1) + "px";
  }

  function closeDragElement() {
    // stop moving when mouse button is released:
    document.onmouseup = null;
    document.onmousemove = null;
  }
}

var danishLink = document.getElementById('danishLink');
var swedishLink = document.getElementById('swedishLink');
var norwegianLink = document.getElementById('norwegianLink');

var danishFlag = document.getElementById('denmark');
var swedishFlag = document.getElementById('sweden');
var norwegianFlag = document.getElementById('norway');
danishFlag.style.display = "none";
swedishFlag.style.display = "none";
norwegianFlag.style.display = "none";

danishLink.addEventListener('click',ev=>{
    danishFlag.style.display="inline";
});
swedishLink.addEventListener('click',ev=>{
    swedishFlag.style.display="inline";

});
norwegianLink.addEventListener('click',ev=>{
    norwegianFlag.style.display="inline";
});

danishFlag.addEventListener('click',ev=>{
    danishFlag.style.display="none";
});
swedishFlag.addEventListener('click',ev=>{
    swedishFlag.style.display="none";
});
norwegianFlag.addEventListener('click',ev=>{
    norwegianFlag.style.display="none";
});

