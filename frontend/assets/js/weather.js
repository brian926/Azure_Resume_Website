const list = document.querySelector(".ajax-section .cities");
const funcUrl = 'https://test2weatherfun.azurewebsites.net/api/WeatherFunction?';

document.addEventListener('DOMContentLoaded', e => {
    e.preventDefault();

    var cityList = [];
    var defaultCities = ["new%20york", "san%20francisco", "nashville", "boston", "seattle", "chicago"];
    defaultCities.forEach(element => {
        cityList.push(`${funcUrl}?name=${element}`);
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
                        <div class="city-temp">${Math.round(main.temp)}<sup>°F</sup></div>
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
});