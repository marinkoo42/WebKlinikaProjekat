import { ListaKlinikaOdeljenje } from './ListaKlinikaOdeljenje.js'
import { ListaRezervacija} from './ListaRezervacija.js'

let proveraRezervacije = (email, idGrada , idKlinike) => {
    document.body.innerHTML = '';
    mainMenu(idGrada , idKlinike);
    let container = document.querySelector(".container");
    let flexContainer = document.createElement("div");
    flexContainer.classList.add("flexContainer");

    let lista = new ListaRezervacija(email);
    lista.loadData();

    lista.draw(flexContainer);
    container.appendChild(flexContainer);
}

let odeljenja = (idGrada,idKlinike) => {

    document.body.innerHTML = '';
    mainMenu( idGrada , idKlinike);
  

    let container = document.querySelector(".container");

    let veze = document.createElement('div');
    veze.classList.add("veze");

    let dp = document.createElement("div");
    dp.classList.add("dp");

    let termini = document.createElement("div");
    termini.classList.add("termini");

    let forma = document.createElement("div");
    forma.setAttribute("class", "forma");
    forma.setAttribute("style", "padding:1rem;");

    container.appendChild(veze);
    container.appendChild(dp);
    container.appendChild(termini);
    container.appendChild(forma);


    let table = document.querySelector(".veze");
    table.innerHTML = '';
    let listaKlinikaOdeljenja = new ListaKlinikaOdeljenje();

    listaKlinikaOdeljenja.loadData(idKlinike);
    listaKlinikaOdeljenja.draw(table);
}



let mainMenu = ( prevSelectedGrad, prevSelectedKlinika) => {
    let header = document.createElement("header");
    header.classList.add("vrh");
    header.classList.add("bg-primary");
    let logo = document.createElement("img");
    logo.src = "./Images/logo.png";

    header.appendChild(logo);

    let container = document.createElement('div');
    container.setAttribute("class", "container");

    let selectGrad = document.createElement("select");
    selectGrad.setAttribute("class", "form-select lista-gradova");
    selectGrad.setAttribute("aria-label", "Izaberite grad!");

    let selectKlinika = document.createElement("select");
    selectKlinika.setAttribute("class", "form-select lista-klinika");
    selectKlinika.setAttribute("aria-label", "Izaberite kliniku!");

    let klinikaButton = document.createElement("button");
    klinikaButton.setAttribute("type", "button");
    klinikaButton.setAttribute("class", "btn btn-primary");
    klinikaButton.innerHTML = "Pretraži odeljenja u klinici!";
    klinikaButton.setAttribute("style", "margin-bottom:1rem;");

    let proveraInput = document.createElement("input");
    proveraInput.type = "email";
    proveraInput.classList.add("form-control");
    proveraInput.placeholder = "Unesite email na koji ste zakazali!";

    let proveraButton = document.createElement("button");
    proveraButton.innerText = "Provera zakazivanja!";
    proveraButton.setAttribute("type", "button");
    proveraButton.setAttribute("class", "btn btn-info");

    

    let row = document.createElement('div');
    row.setAttribute("class", "row");
    row.setAttribute("style", "padding:1rem;");

    let col1 = document.createElement('div');
    col1.setAttribute("class", "col-xl-6");
    let col2 = document.createElement('div');
    col2.setAttribute("class", "col-xl-6");

    col1.appendChild(selectGrad);

    col1.appendChild(selectKlinika);
    col1.appendChild(klinikaButton);

    col2.appendChild(proveraInput);
    col2.appendChild(proveraButton);

    row.appendChild(col1);
    row.appendChild(col2);

    container.appendChild(row);

        fetch('https://localhost:5001/server/Gradovi/GetGradovi')
            .then(data => data.json())
            .then(data => {
                data.forEach(e => {
                    let option = document.createElement("option");
                    option.id = e.id;
                    option.value = e.id;
                    option.innerHTML = e.imeGrada;
                    if (option.value == prevSelectedGrad)
                        option.selected = true;
                    selectGrad.appendChild(option);
                });
                selectGrad.onchange();
            })
            .catch(err => console.error(err));

    
    selectGrad.onchange = () => {
        selectKlinika.length = 0;
        fetch('https://localhost:5001/server/Klinika/GetKlinike/' + selectGrad.value)
            .then(data => data.json())
            .then(data => {
                data.forEach(e => {
                    let option = document.createElement("option");
                    option.id = e.id;
                    option.value = e.id;
                    option.innerHTML = e.nazivKlinike;
                    if (option.value == prevSelectedKlinika)
                        option.selected = true;
                    selectKlinika.appendChild(option);

                });
            })
            .catch(err => {
                alert("Nema klinika u ovom gradu!");
                console.error(err);
            });
    }
    

    klinikaButton.onclick = () => {
        prevSelectedGrad = selectGrad.value;
        prevSelectedKlinika = selectKlinika.value;
        odeljenja(selectGrad.value, selectKlinika.value);
    }

    proveraButton.onclick = () => {
        prevSelectedGrad = selectGrad.value;
        prevSelectedKlinika = selectKlinika.value;
        proveraRezervacije(proveraInput.value, prevSelectedGrad, prevSelectedKlinika);
    }
    document.body.appendChild(header);
    document.body.appendChild(container);
}

document.body.onload
{
    mainMenu();
}
