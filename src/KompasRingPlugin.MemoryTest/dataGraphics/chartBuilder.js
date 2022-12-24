const fs = require("fs");

function getIndexes(){
    var contents = fs.readFileSync("../itemIndex.txt").toString();
    const arr = contents.split(/\r?\n/).toString().split(",");
    console.log(arr);
}

function getMemoryUsage(){
    var text2 = fs.readFileSync("../memoryUsage.txt");
    var memoryUsage = text2.split("\n");
    return memoryUsage;
}

const http = require("http");
 
http.createServer(function(request, response){
     
    fs.createReadStream("index.html").pipe(response);
}).listen(3000);
