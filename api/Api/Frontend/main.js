fetch('https://localhost:7247/Tasks?userId=21', {
    method: 'GET'
})
    .then(response => response.text())
    .then(text => alert(text))