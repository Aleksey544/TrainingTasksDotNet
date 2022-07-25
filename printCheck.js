function printCheck(productList) {
    function priceCategorySum(category) {
        let priceCategory = 0

        for (let i = 0; i < productList.length; i++)
        {
            if (productList[i].category === category)
                priceCategory += productList[i].price
        }

        return priceCategory
    }

    let categories = new Array()
    let price = 0
    let output = ''

    for (let i = 0; i < productList.length; i++)
    {
        if (!categories.includes(productList[i].category))
            categories.push(productList[i].category)
    }

    for (let i = 0; i < categories.length; i++)
    {
        let switcher = true
        for (let j = 0; j < productList.length; j++)
        {
            if (productList[j].category === categories[i])
            {
                if (switcher)
                {
                    output += '\n' + productList[j].category + '  ' + priceCategorySum(productList[j].category)
                    switcher = false
                }

                output += '\n   ' + productList[j].product + '  ' + productList[j].price
                price += productList[j].price
            }
        }
    }

    output += '\nВсього:  ' + price
    console.log(output)
}

//Tests
let products = new Array(8)
products[0] = { product: 'салат', price: 24, category: 'кухня' }
products[1] = { product: 'пиво', price: 18, category: 'бар' }
products[2] = { product: 'риба', price: 28, category: 'кухня' }
products[3] = { product: 'мило', price: 5, category: 'хімія' }
products[4] = { product: 'вино', price: 70, category: 'бар' }
products[5] = { product: 'фарш', price: 53, category: 'кухня' }
products[6] = { product: 'торт', price: 42, category: 'кондитерські вироби' }
products[7] = { product: 'шампунь', price: 25, category: 'хімія' }

printCheck(products)
