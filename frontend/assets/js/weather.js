const list = document.querySelector(".ajax-section .cities");
const apiKey = "";
const api = "";

document.addEventListener('DOMContentLoaded', e => {
    e.preventDefault();

    $.get(`http://ipinfo.io?token=${api}`, function (response) {

        var loc = response.loc;
        var myArray = loc.split(",");
        let lat = myArray[0];
        let log = myArray[1];

        //Multi City here
        const url = `https://api.openweathermap.org/data/2.5/weather?lat=${lat}&lon=${log}&appid=${apiKey}&units=imperial`;
        var cityList = [];
        cityList.push(url);

        var defaultCities = ["new%20york", "san%20francisco", "nashville", "boston"];
        defaultCities.forEach(element => {
            cityList.push(`https://api.openweathermap.org/data/2.5/weather?q=${element},us&appid=${apiKey}&units=imperial`);
        });

        for (const url of cityList) {
        fetch(url)
            .then(response => response.json())
            .then(data => {
                const { main, name, sys, weather } = data;
                const icon = `https://s3-us-west-2.amazonaws.com/s.cdpn.io/162656/${weather[0]["icon"]}.svg`;
                console.log(icon);

                const li = document.createElement("li");
                li.classList.add("city");
                const markup = `
                            <h4 class="city-name" data-name="${name},${sys.country}">
                              <span>${name}</span>
                              <sup>${sys.country}</sup>
                            </h4>
                            <div class="city-temp">${Math.round(main.temp)}<sup>°C</sup></div>
                            <figure>
                              <img class="city-icon" src="${icon}" alt="${weather[0]["description"]
                                        }">
                              <figcaption>${weather[0]["description"]}</figcaption>
                            </figure>
                          `;
                li.innerHTML = markup;
                list.appendChild(li);
             });
        };
    }, "jsonp");
    
});