const jwtToken = localStorage.getItem('jwtToken');
if (!jwtToken) {
    window.location.href = '/index.html';
} else {
    fetch(`${BASE_URL}/api/v${API_VER}/customer`, {
        method: 'GET',
        headers: {
            'Authorization': `Bearer ${jwtToken}`
        }
    })
        .then(response => response.json())
        .then(data => {
            if (data.status === 401) {
                localStorage.removeItem('jwtToken');
                window.location.replace('view/customer.html');
            }

            var templateSource = document.getElementById('table-data-template').innerHTML;
            var template = Handlebars.compile(templateSource);
            var renderedHtml = template(data);
            document.getElementById('table__body').innerHTML = renderedHtml;

            new DataTable('#customer-table');
        })
        .catch(error => {
            console.error('Error:', error);
        });
}