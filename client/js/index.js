
const handleRegister = () => {
    window.location.replace('view/register.html');
}

document.getElementById('login-form').addEventListener('submit', async function (event) {
    event.preventDefault();

    const email = document.getElementById('email').value;
    const password = document.getElementById('password').value;

    const loginData = {
        email: email,
        password: password
    };

    const url = `${BASE_URL}/api/v${API_VER}/Account/Login`;

    try {
        const response = await fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(loginData)
        });

        const data = await response.json();

        if (response.ok && data.token) {
            console.log(data.token)
            localStorage.setItem('jwtToken', data.token);
            window.location.replace('view/customer.html');
        } else {
            console.log('Invalid credentials');
        }
    } catch (error) {
        console.error('Error:', error);
    }
});
    