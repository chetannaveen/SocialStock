// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function changeme() {
    document.getElementById("refresh").disabled = false;
    document.getElementById("colors").disabled = true;
    var nodes = document.getElementById("colorDiv").getElementsByTagName('li');
    for (var i = 0; i < nodes.length; i++) {
        nodes[i].style.background = '#f3f3f3';
        nodes[i].style.color = '#000000';

    }
}