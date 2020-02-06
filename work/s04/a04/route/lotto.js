const express = require("express");
const router  = express.Router();
	
// Add a route for the path /
router.get("/lotto", (req, res) => {


    res.send(getRandomInt());
});

function getRandomInt() {
    var numbers = [];

    for(var i = 0; i < 7; i++) {
		var add = true;
		var randomNumber = Math.floor(Math.random() * 35) + 1;
		for(var y = 0; y < 35; y++) {
			if(numbers[y] == randomNumber) {
				add = false;
			}
		}
		if(add) {
			numbers.push(randomNumber);
		} else {
			i--;
		}
	}
  
	var highestNumber = 0;
	for(var m = 0; m < numbers.length; m++) {
		for(var n = m + 1; n < numbers.length; n++) {
			if(numbers[n] < numbers[m]) {
				highestNumber = numbers[m];
				numbers[m] = numbers[n];
				numbers[n] = highestNumber;
			}
		}
	}
  

    return numbers.join("-");
  };


module.exports = router;
















