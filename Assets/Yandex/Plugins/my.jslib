mergeInto(LibraryManager.library, {

    /*
    //виндовс алекрт - всплывающее поверх браузера окно с предупреждением
    Hello: function () {
        window.alert("Hello, world!");
        console.log("Тут был Вася 123");
    },
    */

    PlayerData: function () {

        // console.log(player.getName());
        // console.log(player.getPhoto("medium"));


        //myGameInstance.SendMessage('Yandex', 'Set_text_nik', player.getName());
        myGameInstance.SendMessage('Yandex', 'SetNik', player.getName());
        myGameInstance.SendMessage('Yandex', 'Set_Image', player.getPhoto("medium"));

    },

    OzenkaJS: function () {
        ysdk.feedback.canReview()
            .then(({ value, reason }) => {
                if (value) {
                    ysdk.feedback.requestReview()
                        .then(({ feedbackSent }) => {
                            console.log(feedbackSent);
                        })
                } else {
                    console.log(reason)
                }
            })
    },


    SaveJS: function (date) {

        var dateString = UTF8ToString(date);
        //преобразуем строку из ждава скрипта в тот формат, который понимает юнити или наоборот
        var myobj = JSON.parse(dateString);
        //потом эту строку преобразуем в обьект
        //а его сохраняем
        //он содержит пары ключ-значение (деньги - количество денег)
        if (!player) {
            console.warn("player не инициализирован для сохранения");
            myGameInstance.SendMessage('Yandex', 'Set_igrok_avtorizirovan', false); 
            myGameInstance.SendMessage('Yandex', 'Save_PlayerPrefs');
            return;
        }
        if (typeof player.setData !== 'function') {
            console.warn("setData недоступен");
            myGameInstance.SendMessage('Yandex', 'Set_igrok_avtorizirovan', false); 
            myGameInstance.SendMessage('Yandex', 'Save_PlayerPrefs');
            return;
        }

        player.setData(myobj)
            .then(() => {
                console.log("Данные сохранены");
            })
            .catch(err => {
                console.warn("Ошибка при сохранении данных:", err);
            });

    },
    LoadJS: function () {
        console.log("3 JS Загрузка запущена")
        //проверка есть ли игрок
        if (!player) {
            console.warn("player не инициализирован для загрузки");
            myGameInstance.SendMessage('Yandex', 'Set_igrok_avtorizirovan', false); 
            myGameInstance.SendMessage('Yandex', 'Load_Yandex', null);
            return;
        }
        if (typeof player.getData !== 'function') {
            console.warn("getData недоступен");
            myGameInstance.SendMessage('Yandex', 'Set_igrok_avtorizirovan', false); 
            myGameInstance.SendMessage('Yandex', 'Load_Yandex', null);
            return;
        }
        player.getData()
            .then(_date => {

                const myJSON = JSON.stringify(_date);
                console.log("4 JS данные получены с сервера")
                myGameInstance.SendMessage('Yandex', 'Load_Yandex', myJSON);
                //передача данных в C#, где
                //1 - это обьект на котором висит скрипт
                //2 - вызываемая функция
                //3 - то, что мы в неё передаём
            }
            )
            .catch(error => {
                console.warn("Ошибка при получении данных игрока:", error);
                myGameInstance.SendMessage('Yandex', 'Set_igrok_avtorizirovan', false); 
                myGameInstance.SendMessage('Yandex', 'Load_Yandex', null);
            });

    },
    //get_ustroistvo_string: function () {
    //    console.log("KIR Poluchit_ustroistvo");
    //    console.log(navigator.userAgent);
    //    return navigator.userAgent;
    //    // return /iPhone|iPad|iPod|tablet|Android/i.test(navigator.userAgent);
    //},
    get_ustroistvo_mobile: function () {
        return /iPhone|iPad|iPod|tablet|Android/i.test(navigator.userAgent);
    }




});
