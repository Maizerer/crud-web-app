// Получение всех отелей
async function Getgood() {
    // отправляет запрос и получаем ответ
    const response = await fetch("/api/goods", {
        method: "GET",
        headers: { "Accept": "application/json" }
});
// если запрос прошел нормально
if (response.ok === true) {
    // получаем данные
    const goods = await response.json();
    let rows = document.querySelector("tbody");
    goods.forEach(good => {
        // добавляем полученные элементы в таблицу
        rows.append(row(good));
    });
}
}
// Получение одного отеля
async function GetgoodById(id) {
    const response = await fetch("/api/goods/" + id, {
        method: "GET",
        headers: { "Accept": "application/json" }
    });
    if (response.ok === true) {
        const good = await response.json();
        const form = document.forms["goodForm"];
        form.elements["Id"].value = good.id;
        form.elements["Name"].value = good.name;
        form.elements["Count"].value = good.count;
        form.elements["Price"].value = good.price;
    }
}
async function Creategood(goodName, goodCount,
    goodPrice) {
    const response = await fetch("api/goods", {
        method: "POST",
headers: {
            "Accept": "application/json", "Content-Type":
                "application/json"
        },
        body: JSON.stringify({
            Name: goodName,
            Count: parseInt(goodCount, 10),
            Price: parseInt(goodPrice, 10),
})
});
    if (response.ok === true) {
        const good = await response.json();
        reset();
        document.querySelector("tbody").append(row(good));
    }
}
async function Editgood(goodId, goodName, goodCount,
    goodPrice) {
    const response = await fetch("/api/goods/" + goodId, {
        method: "PUT",
headers: {
        "Accept": "application/json", "Content-Type":
            "application/json"
    },
        body: JSON.stringify({
            Id: parseInt(goodId, 10),
            Name: goodName,
            Count: parseInt(goodCount, 10),
            Price: parseInt(goodPrice, 10)
})
});
if (response.ok === true) {
    const good = await response.json();
    reset();
    document.querySelector("tr[data-rowid='" + good.id +
        "']").replaceWith(row(good));
}}
// Удаление пользователя
async function Deletegoods(id) {
    const response = await fetch("/api/goods/" + id, {
        method: "DELETE",
        headers: { "Accept": "application/json" }
});
if (response.ok === true) {
    const good = await response.json();
    document.querySelector("tr[data-rowid='" + good.id +
        "']").remove();
}
}
// сброс формы
function reset() {
    const form = document.forms["goodForm"];
    form.reset();
    form.elements["Id"].value = 0;
}
// создание строки для таблицы
function row(good) {
    const tr = document.createElement("tr");
    tr.setAttribute("data-rowid", good.id);
    const idTd = document.createElement("td");
    idTd.append(good.id);
    tr.append(idTd);
    const nameTd = document.createElement("td");
    nameTd.append(good.name);
    tr.append(nameTd);
    const countTd = document.createElement("td");
    countTd.append(good.count);
    tr.append(countTd);
    const priceTd = document.createElement("td");
    priceTd.append(good.price + ' ₽');
    tr.append(priceTd);
    const linksTd = document.createElement("td");
    const editLink = document.createElement("a");
    editLink.setAttribute("data-id", good.id);
    editLink.setAttribute("style", "margin:0 10px;");
    editLink.classList.add('btn')
    editLink.classList.add('btn-primary')
    editLink.append("Изменить");
    editLink.addEventListener("click", e => {
        e.preventDefault();
        GetgoodById(good.id);
    });
    linksTd.append(editLink);
    const removeLink = document.createElement("button");
    removeLink.setAttribute("data-id", good.id);
    removeLink.setAttribute("style", "margin:0 10px;");
    removeLink.classList.add('btn')
    removeLink.classList.add('btn-danger')
    removeLink.append("Удалить");
    removeLink.addEventListener("click", e => {
        e.preventDefault();
        Deletegoods(good.id);
    });
    linksTd.append(removeLink);
    tr.appendChild(linksTd);
    return tr;
}
function InitialFunction() {
    // сброс значений формы
    document.getElementById("reset").click(function (e) {
        e.preventDefault();
        reset();
    })
    // отправка формы
    document.forms["goodForm"].addEventListener("submit", e => {
        e.preventDefault();
        const form = document.forms["goodForm"];
        const Id = form.elements["Id"].value;
        const Name = form.elements["Name"].value;
        const Count = form.elements["Count"].value;
        const Price = form.elements["Price"].value;
        if (Id == 0)
            Creategood(Name, Count, Price);
        else {
            Editgood(Id, Name, Count, Price);
        }
    });
    Getgood();
}