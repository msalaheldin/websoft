/**
 * A function to wrap it all in.
 */



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
function moveTheDuck() {
    var left = "style='float: right;'";
    var right = "style='float: left;'";
    var duckPos = document.getElementById("duck").style.cssFloat;

    if(duckPos == "left"){
        document.getElementById("duck").style.cssFloat = "right";
    }else {
        document.getElementById("duck").style.cssFloat = "left";
    }
        
}
function hideTheDuck(){

    document.getElementById("duck").style.display = "none";
}
