let getAllBtn = document.getElementById("getAllUsersBtn");
let getUserByIdBtn = document.querySelector("#getUserByIdBtn");
let createUserBtn = document.querySelector("#createUserBtn");

let userIdInput = document.querySelector("#userIdInput");
let userFirstName = document.querySelector("#firstName");
let userLastName = document.querySelector("#lastName");

let port = "5080";
let getAllUsers = async () => {
    try {
        let url = "http://localhost:" + port + "/api/users";
        let response = await fetch(url);
        let data = await response.json();
        console.log(data);
    }
    catch (err) {
        console.log(err)
    }
};

let getUserById = async () => {
    try {
        let url = "http://localhost:" + port + "/api/users/" + userIdInput.value;
        let response = await fetch(url);
        let data = await response.text();
        console.log(data);
    }
    catch (err) {
        console.log(err)
    }

};

let createUser = async () => {
    let url = "http://localhost:" + port + "/api/users/createUser";
    console.log({ FirstName: userFirstName.value, LastName: userLastName.value })
    var response = await fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': "application/json"
        },
        body: JSON.stringify({ FirstName: userFirstName.value, LastName: userLastName.value })
    });
    var data = await response.text();
    console.log(data);
}


getAllBtn.addEventListener("click", getAllUsers);
getUserByIdBtn.addEventListener("click", getUserById);
createUserBtn.addEventListener("click", createUser)