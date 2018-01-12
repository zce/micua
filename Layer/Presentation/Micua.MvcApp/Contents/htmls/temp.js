var frameRate = 20;
var increment = 0.5;
var max = 5;
var min = 1;

// 先拿到canvas 的 Dom对象
var canvas = document.getElementById('bg_canvas');
var context = canvas.getContext('2d');

var dots = [];
function begin() {
    canvas.width = canvas.clientWidth;
    canvas.height = canvas.clientHeight;
    //canvas.style.backgroundColor = '#444349';
    // 拿到绘图上下文对象

    var count = canvas.clientWidth / 5;
    dots = [];
    for (var i = getRangeRandom(count - 100, count) ; i >= 0 ; i--) {
        var color;
        switch (i % 5) {
            case 0:
                color = "#C2F012";
                break;
            case 1:
                color = "#87F2D4";
                break;
            case 2:
                color = "#C1E6E2";
                break;
            case 3:
                color = "#C2CDCF";
                break;
            case 4:
                color = "#679EB8";
                break;
        }
        var r = getRangeRandom(min, max);
        var dot = {
            position: { x: getRangeRandom(0, canvas.width), y: getRangeRandom(0, canvas.height) },
            velocity: { x: 0, y: 0 },
            style: color,
            radius: r,
            increase: r < max / 2
        }
        dots.push(dot);
    }
}

begin();

setInterval(function () {
    update();
    render();
}, 1000 / frameRate);

window.onresize = function () {
    begin();
};

function update() {
    each(function (i, item) {
        // 闪烁
        if (item.increase) {
            item.radius += increment;
            if (item.radius > max) {
                item.increase = false;
            }
        } else {
            item.radius -= increment;
            if (item.radius < min) {
                item.increase = true;
            }
        }
    });
}

function render() {
    context.clearRect(0, 0, canvas.width, canvas.height);
    each(function (i, item) {
        context.fillStyle = item.style;
        context.beginPath();
        context.arc(item.position.x, item.position.y, item.radius, 0, 2 * Math.PI);
        context.closePath();
        context.fill();
    });
}

function each(func) {
    for (var i = 0; i < dots.length; i++) {
        func(i, dots[i]);
    }
}

function getRangeRandom(min, max) {
    return parseInt(Math.random() * (max - min) + min);
}