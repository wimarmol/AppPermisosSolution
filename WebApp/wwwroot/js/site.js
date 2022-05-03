
const dateSet = (date) => {
    let day = ("0" + date.getDate()).slice(-2);
    let month = ("0" + (date.getMonth() + 1)).slice(-2);   
    return `${date.getFullYear()}-${month}-${day}`;
}

const dateTimeSet = (dateTime) => {
    var day = ("0" + dateTime.getDate()).slice(-2);
    var month = ("0" + (dateTime.getMonth() + 1)).slice(-2);
    var hours = ("0" + (dateTime.getHours() + 1)).slice(-2);
    var minutes = ("0" + (dateTime.getMinutes() + 1)).slice(-2);
    return `${dateTime.getFullYear()}-${month}-${day}T${hours}:${minutes}`;    
}

const dateSetStandar = (date) => {
    var day = ("0" + date.getDate()).slice(-2);
    var month = ("0" + (date.getMonth() + 1)).slice(-2);
    return `${day}/${month}/${date.getFullYear()}`;
}

const dateTimeSetStandar = (dateTime) => {
    var day = ("0" + dateTime.getDate()).slice(-2);
    var month = ("0" + (dateTime.getMonth() + 1)).slice(-2);
    var hours = ("0" + (dateTime.getHours() + 1)).slice(-2);
    var minutes = ("0" + (dateTime.getMinutes() + 1)).slice(-2);
    return `${day}/${month}/${dateTime.getFullYear()} ${hours}:${minutes}:00`;
}

const alertMessage = (idInput, idAlert, message) => {
    document.getElementById(idInput).className += " error";
    document.getElementById(idAlert).classList.remove("hide");
    document.getElementById(idAlert).textContent = message;
    setTimeout(()=> {
        document.getElementById(idInput).classList.remove("error");
        document.getElementById(idAlert).className += " hide";
        document.getElementById(idAlert).textContent = "";
        document.getElementById(idInput).focus();
    }, 2100);

};

const message = (idAlert, message) => {    
    document.getElementById(idAlert).classList.remove("hide");
    document.getElementById(idAlert).textContent = message;
    setTimeout(()=> {    
        document.getElementById(idAlert).className += " hide";
        document.getElementById(idAlert).textContent = "";
    }, 3000);
};

const buildTableCatalog = (dataset = [] , table, editFunction, deleteFunction) => {
    table.innerHTML = '';
    const thead_ = document.createElement("thead");   
    const tbody_ = document.createElement("tbody");
    table.append(thead_);
    table.append(tbody_);    
    const thead = table.querySelector("thead");
    const tbody = table.querySelector("tbody");    
    dataset.forEach((item, index) => {
        const row = document.createElement("tr");
        for (var prop in item) {
            if (prop !== 'id') {
                if (index === 0) {
                    const th = document.createElement("th");
                    th.innerText = prop.charAt(0).toUpperCase() + prop.slice(1);;
                    thead.append(th);
                };
                const td = document.createElement("td");
                if (prop.includes('fecha')){
                    td.innerText = dateSetStandar(new Date(item[prop]));
                } else {
                    td.innerText = item[prop];
                }
                row.append(td);
            }
        };
        if (index === 0) {            
            const thAction = document.createElement("th");
            thAction.className = "center";
            thead.append(thAction);
        };
        const tdAction = document.createElement("td");
        tdAction.className = "center";
        const btnEdit = document.createElement("button");
        const btnDelete = document.createElement("button");
        btnDelete.className = "bnt btn-sm btn-outline-primary ml-2";
        btnEdit.className = "bnt btn-sm btn-outline-danger pl-2";
        btnEdit.textContent = "Editar";
        btnDelete.textContent = "Eliminar";
        btnEdit.onclick = () => {
            editFunction(item);
        };
        btnDelete.onclick = () => {
            deleteFunction(item);
        };
        tdAction.append(btnEdit);
        tdAction.append(btnDelete);
        row.append(tdAction);
        tbody.append(row);       
    });
    table.append(tbody);
}

const add_td = (row, td, item,  prop,) => {
    td = document.createElement("td");
    td.innerText = item[prop];
    row.append(td);
};