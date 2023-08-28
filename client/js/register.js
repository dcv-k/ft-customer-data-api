document.getElementById('register-form').addEventListener('submit', async function (event) {
    event.preventDefault();

    const email = document.getElementById('email').value;
    const password = document.getElementById('password').value;
    const roles = [document.getElementById('roles').value];

    const registerData = {
        email: email,
        password: password,
        roles: roles
    };

    const url = `${BASE_URL}/api/v${API_VER}/Account/Register`;

    try {
        const response = await fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(registerData)
        });

        if (response.ok) {
            window.location.replace('../index.html');
            console.log('Registration successful');
        } else {
            console.log('Registration failed');
        }
    } catch (error) {
        console.error('Error:', error);
    }
});