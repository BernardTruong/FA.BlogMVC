
/*!
* Start Bootstrap - Clean Blog v6.0.9 (https://startbootstrap.com/theme/clean-blog)
* Copyright 2013-2023 Start Bootstrap
* Licensed under MIT (https://github.com/StartBootstrap/startbootstrap-clean-blog/blob/master/LICENSE)
*/
window.addEventListener('DOMContentLoaded', () => {
    let scrollPos = 0;
    const mainNav = document.getElementById('mainNav');
    const headerHeight = mainNav.clientHeight;
    window.addEventListener('scroll', function () {
        const currentTop = document.body.getBoundingClientRect().top * -1;
        if (currentTop < scrollPos) {
            // Scrolling Up
            if (currentTop > 0 && mainNav.classList.contains('is-fixed')) {
                mainNav.classList.add('is-visible');
            } else {
                console.log(123);
                mainNav.classList.remove('is-visible', 'is-fixed');
            }
        } else {
            // Scrolling Down
            mainNav.classList.remove(['is-visible']);
            if (currentTop > headerHeight && !mainNav.classList.contains('is-fixed')) {
                mainNav.classList.add('is-fixed');
            }
        }
        scrollPos = currentTop;
    });
})


//document.addEventListener('DOMContentLoaded', function () {
//    var videoElement = document.getElementById('_video');
//    fetch('http://commondatastorage.googleapis.com/codeskulptor-demos/DDR_assets/Kangaroo_MusiQue_-_The_Neverwritten_Role_Playing_Game.mp3')
//        .then(response => response.blob())
//        .then(blob => {
//            const url = URL.createObjectURL(blob);
//            // Now 'url' can be used as the Blob URL for the MP4 file
//            videoElement.src = url;
//        })
//        .catch(error => console.error('Error fetching MP4 file:', error));
//});

//document.addEventListener('DOMContentLoaded', function () {
//    var videoElement = document.getElementById('myVideo');
//    var videoUrl = 'https://www.w3schools.com/html/mov_bbb.mp4';

//    fetch(videoUrl)
//        .then(response => {
//            if (!response.ok) {
//                throw new Error('Network response was not ok');
//            }
//            return response.blob(); // Simplified this line to directly get the blob
//        })
//        .then(blob => {
//            var blobUrl = URL.createObjectURL(blob);
//            videoElement.src = blobUrl;
//        })
//        .catch(error => console.error('Error streaming video:', error));
//});

//document.addEventListener('DOMContentLoaded', function () {
//    var videoElement = document.getElementById('myVideo');
//    var videoUrl = 'https://files.vietales.vn/asset/podcast/hwzEdwDTRihifGU.mp3';

//    if ('MediaSource' in window && MediaSource.isTypeSupported('audio/mpeg')) {
//        var mediaSource = new MediaSource();
//        videoElement.src = URL.createObjectURL(mediaSource);

//        mediaSource.addEventListener('sourceopen', function () {
//            var sourceBuffer = mediaSource.addSourceBuffer('audio/mpeg');

//            fetch(videoUrl)
//                .then(response => response.body)
//                .then(body => {
//                    var reader = body.getReader();

//                    function read() {
//                        reader.read().then(({ done, value }) => {
//                            if (done) {
//                                mediaSource.endOfStream();
//                            } else {
//                                sourceBuffer.appendBuffer(value);
//                                read();
//                            }
//                        });
//                    }

//                    read();
//                })
//                .catch(error => console.error('Error streaming audio:', error));
//        });
//    } else {
//        console.error('MediaSource or audio/mpeg not supported in this browser.');
//    }
//});

//window.onload = () => {
//    const audio = document.getElementById("my-audio");
//    const canvas = document.getElementById("my-canvas");
//    const context = canvas.getContext("2d");

//    context.fillStyle = "lightgray";
//    context.fillRect(0, 0, canvas.width, canvas.height);
//    context.fillStyle = "red";
//    context.strokeStyle = "white";

//    const inc = canvas.width / audio.duration;

//    // Display TimeRanges

//    audio.addEventListener("seeked", () => {
//        for (let i = 0; i < audio.buffered.length; i++) {
//            const startX = audio.buffered.start(i) * inc;
//            const endX = audio.buffered.end(i) * inc;
//            const width = endX - startX;

//            context.fillRect(startX, 0, width, canvas.height);
//            context.rect(startX, 0, width, canvas.height);
//            context.stroke();
//        }
//    });
//};
//function createObjectURL(object) {
//    return (window.URL) ? window.URL.createObjectURL(object) : window.webkitURL.createObjectURL(object);
//}

//async function display(videoStream) {
//    var video = document.getElementById('_video');
//    let blob = await fetch(videoStream).then(r => r.blob());
//    var videoUrl = createObjectURL(blob);
//    video.src = videoUrl;
//}
//display('https://files.vietales.vn/asset/podcast/hwzEdwDTRihifGU.mp3');

//function getBase64(url) {
//    return axios
//        .get(url, {
//            responseType: 'arraybuffer'
//        })
//        .then(response => Buffer.from(response.data, 'binary').toString('base64'))
//}
//=================================
function getBase64(url) {
    return axios
        .get(url, {
            responseType: 'arraybuffer'
        })
        .then(response => {
            const arrayBufferView = new Uint8Array(response.data);
            const blob = new Blob([arrayBufferView], { type: response.headers['content-type'] });
            return new Promise((resolve, reject) => {
                const reader = new FileReader();
                reader.onload = function () {
                    resolve(reader.result.split(',')[1]);
                };
                reader.onerror = function (error) {
                    reject(error);
                };
                reader.readAsDataURL(blob);
            });
        });
}

function convertDataURIToBinary(dataURI) {
    var BASE64_MARKER = ';base64,';
    var base64Index = dataURI.indexOf(BASE64_MARKER) + BASE64_MARKER.length;
    var base64 = dataURI.substring(base64Index);
    var raw = window.atob(base64);
    var rawLength = raw.length;
    var array = new Uint8Array(new ArrayBuffer(rawLength));

    for (i = 0; i < rawLength; i++) {
        array[i] = raw.charCodeAt(i);
    }
    return array;
}

document.addEventListener('DOMContentLoaded', function () {
    var videoElement = document.getElementById('_video');
    //var url = 'https://files.heavensbride.org/index.php/s/q5jKHiLE7qFSctc/download';
    //console.log(url);
    //var base64Audio = getBase64(url);
    //console.log(base64Audio);
    //var sample = "data:audio/ogg;base64," + base64Audio;
    //console.log(sample)
    //Replace 'your_audio_url' with the actual URL of the audio file
    var binary = convertDataURIToBinary(data);
    var blob = new Blob([binary], { type: "video/mp4" });
    var blobUrl = URL.createObjectURL(blob); videoElement.src = blobUrl;
});