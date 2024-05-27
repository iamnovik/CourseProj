function updateCollectionName(input) {

    var newValue = input.value;
    var id = input.dataset.id;
    // Здесь вызывается метод обновления данных в БД
    updateDatabase(newValue,id, "Collection", "UpdateCollectionName");
}

function updateCollectionDescriptionDb(input) {

    var newValue = input.value;
    var id = input.dataset.id;
    // Здесь вызывается метод обновления данных в БД
    updateDatabase(newValue,id, "Collection", "UpdateCollectionDescription");
}

function updateItemName(input) {

    var newValue = input.value;
    var id = input.dataset.id;
    console.log(newValue)
    console.log(id)
    // Здесь вызывается метод обновления данных в БД
    updateDatabase(newValue,id, "Item", "UpdateItemName");
}

function updateAttributeValue(input){
    var newValue
    if (input.type === 'checkbox'){
        console.log("update checkbox")
        newValue = $(input).is(':checked').toString();
    }else{
        newValue = input.value.toString()
    }
    var id = input.dataset.id;
    
    updateDatabase(newValue, id, "ItemAttribute", "UpdateItemAttributeValue")
}
function updateCollectionImage(form){

    $.ajax({
        url: `/Collection/UpdateCollectionImage`,  // URL вашего метода обновления на сервере
        type: 'POST',
        data: form ,
        processData: false, // Не обрабатывайте данные formData
        contentType: false, // Не устанавливайте тип содержимого
        success: function(response) {
            location.reload()
        }
    });
}
function updateDatabase(newValue, id, controller, method) {
    $.ajax({
        url: `/${controller}/${method}`,  // URL вашего метода обновления на сервере
        type: 'POST',
        data: {
            value: newValue,
            id: id
        },
        success: function(response) {
            console.log(response);
        }
    });
}