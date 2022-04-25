const btn = document.getElementById("view_btn");
const table = document.getElementById("table");
const chart = document.getElementById("chart_container");
const form = document.getElementById("input_form");
const inputs = document.getElementsByClassName("input");

Array.from(inputs).forEach(input => input.addEventListener("click",
    () => {
        setData(input);
        submitForm();
    }));


window.onload = function () {
    const year = getData("year");
    const month = getData("month");
    const view = getData("view");
    const criteria = getData("criteria");
    if (year != undefined) {
        document.getElementById(year).checked = true;
    }
    if (month != undefined) {
        document.getElementById(month).checked = true;
    }
    if (view != undefined) {
        if (view === "table") {
            btn.innerHTML = "Chart";
            table.classList.remove("hidden");
            chart.classList.add("hidden");
        } else {
            btn.innerHTML = "Table";
            table.classList.add("hidden");
            chart.classList.remove("hidden");
        }
    }
    if (criteria != undefined) {
        let btn = document.createElement("button");
        btn.innerHTML = criteria;
        chooseCriteria(btn);
    }
}


function submitForm() {
    const yearInputs = document.getElementsByClassName("year_input");
    const arrayOfYearInputs = Array.from(yearInputs);

    if (!arrayOfYearInputs.some(input => input.checked === true)) {
        Array.from(document.getElementsByClassName("year_label")).forEach(input => {
            input.classList.add("transition");
            setTimeout(() => {
                input.classList.remove("transition");
            }, 1000);
        });
        return;
    }
    form.submit();
}

function setData(elem) {
    sessionStorage.setItem(elem.getAttribute("name"), elem.getAttribute("id"));
}

function getData(item) {
    return sessionStorage.getItem(item);
}

function onViewTypeClick() {
    if (btn.innerHTML === "Chart") {
        sessionStorage.setItem("view", "chart_container");
        btn.innerHTML = "Table";
        table.classList.add("hidden");
        chart.classList.remove("hidden");
        return true;
    } else if (btn.innerHTML === "Table") {
        sessionStorage.setItem("view", "table");
        btn.innerHTML = "Chart";
        table.classList.remove("hidden");
        chart.classList.add("hidden");
        return true;
    }
    return false;
}

function toMonthName(month) {
    const date = new Date();
    date.setMonth(month - 1);

    return date.toLocaleString('en-US',
        {
            month: 'long',
        });
}

function createChart(modelData, label, labels) {
    let data = {
        label: label,
        data: modelData,
        fill: false,
        borderColor: "#c9e9ff",
        backgroundColor: "transparent",
    };

    new Chart("chart",
        {
            type: "line",
            data: {
                labels: labels,
                datasets: [data]
            }
        });
}

function isValidFileType() {
    const file = document.getElementById("file-upload");
    let fileExtension = file.value.split('.').pop();
    if (fileExtension !== "xlsx" && fileExtension !== "xls") {
        alert("One or more input files are not supported.\nSupported types: .xlsx, .xls");
        file.value = "";
        return false;
    }
    return true;
}

function areFilesAttached() {
    const file = document.getElementById("file-upload");
    if (!file.value) {
        alert("Please, choose file(s).");
        return false;
    }

    return true;
}