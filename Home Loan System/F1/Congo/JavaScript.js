JS Options
13
Confettiful.prototype._setupElements = function () {
    14
    const containerEl = document.createElement('div');
    15
    const elPosition = this.el.style.position;
    16

    17
    if (elPosition !== 'relative' || elPosition !== 'absolute') {
        18
        this.el.style.position = 'relative';
        19
    }
    20

    21
    containerEl.classList.add('confetti-container');
    22

    23
    this.el.appendChild(containerEl);
    24

    25
    this.containerEl = containerEl;
    26
};
27

28
Confettiful.prototype._renderConfetti = function () {
    29
    this.confettiInterval = setInterval(() => {
        30
        const confettiEl = document.createElement('div');
        31
        const confettiSize = (Math.floor(Math.random() * 3) + 7) + 'px';
        32
        const confettiBackground = this.confettiColors[Math.floor(Math.random() * this.confettiColors.length)];
        33
        const confettiLeft = (Math.floor(Math.random() * this.el.offsetWidth)) + 'px';
        34
        const confettiAnimation = this.confettiAnimations[Math.floor(Math.random() * this.confettiAnimations.length)];
        35

        36
        confettiEl.classList.add('confetti', 'confetti--animation-' + confettiAnimation);
        37
        confettiEl.style.left = confettiLeft;
        38
        confettiEl.style.width = confettiSize;
        39
        confettiEl.style.height = confettiSize;
        40
        confettiEl.style.backgroundColor = confettiBackground;
        41

        42
        confettiEl.removeTimeout = setTimeout(function () {
            43
            confettiEl.parentNode.removeChild(confettiEl);
            44
        }, 3000);
        45

        46
        this.containerEl.appendChild(confettiEl);
        47
    }, 25);
    48
};
49

50
window.confettiful = new Confettiful(document.querySelector('.js-container'));
51

52

53


////// Click "Congratulations!" to play animation

////$(function () {
////	var numberOfStars = 20;

////	for (var i = 0; i < numberOfStars; i++) {
////		$('.congrats').append('<div class="blob fa fa-star ' + i + '"></div>');
////	}

////	animateText();

////	animateBlobs();
////});

////$('.congrats').click(function () {
////	reset();

////	animateText();

////	animateBlobs();
////});

////function reset() {
////	$.each($('.blob'), function (i) {
////		TweenMax.set($(this), { x: 0, y: 0, opacity: 1 });
////	});

////	TweenMax.set($('h1'), { scale: 1, opacity: 1, rotation: 0 });
////}

////function animateText() {
////	TweenMax.from($('h1'), 0.8, {
////		scale: 0.4,
////		opacity: 0,
////		rotation: 15,
////		ease: Back.easeOut.config(4),
////	});
////}

////function animateBlobs() {

////	var xSeed = _.random(350, 380);
////	var ySeed = _.random(120, 170);

////	$.each($('.blob'), function (i) {
////		var $blob = $(this);
////		var speed = _.random(1, 5);
////		var rotation = _.random(5, 100);
////		var scale = _.random(0.8, 1.5);
////		var x = _.random(-xSeed, xSeed);
////		var y = _.random(-ySeed, ySeed);

////		TweenMax.to($blob, speed, {
////			x: x,
////			y: y,
////			ease: Power1.easeOut,
////			opacity: 0,
////			rotation: rotation,
////			scale: scale,
////			onStartParams: [$blob],
////			onStart: function ($element) {
////				$element.css('display', 'block');
////			},
////			onCompleteParams: [$blob],
////			onComplete: function ($element) {
////				$element.css('display', 'none');
////			}
////		});
////	});
////}