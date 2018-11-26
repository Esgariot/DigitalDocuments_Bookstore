import xmltodict
from yattag import Doc


#XML data
orderNumber = ''
orderDate = ''
shippingAddress = ''
billingAddress = ''
orderedBooks = ''

with open(sys.argv[1]) as fd:
    doc = xmltodict.parse(fd.read())

orderNumber = doc['PurchaseOrder']['@PurchaseOrderNumber']
orderDate = doc['PurchaseOrder']['@OrderDate']

for address in doc['PurchaseOrder']['Address']:
    if address['@Type'] == 'Shipping':
        shippingAddress = address
    else:
        billingAddress = address

if len(doc['PurchaseOrder']['Items']) > 1:
    for order in doc['PurchaseOrder']['Items']['Item']:
        orderedBooks = order
else:
    orderedBooks = doc['PurchaseOrder']['Items']['Item']

#HTML

doc, tag, text = Doc().tagtext()

doc.asis('<!DOCTYPE html>')

with tag('html'):
    with tag('head'):
        doc.asis('<link rel="stylesheet" type="text/css" href="PurchaseOrderStyle.css">')

        with tag('title'):
            text('Purchase Order')         
    with tag('body'):
        with tag('h1'):
            text('PURCHASE ORDER')
        with tag('h2'):
            text('DATE ' + orderDate)
        with tag('h2'):
            text('PO# ' + orderNumber)  

        for key, attribute in shippingAddress.items():
            if(key == '@Type'):
                with tag('h3'):
                    text(attribute)
            else:
                with tag ('p', ):
                    text(attribute)    
        
        with tag('br'):
            text()
            
        for key, attribute in billingAddress.items():
            if(key == '@Type'):
                with tag('h3'):
                    text(attribute)
            else:
                with tag ('p'):
                    text(attribute)    

        with tag('br'):
            text()

        with tag('table'):
            with tag('tr', klass='table_headers'):
                with tag('th'):
                    text('Product ID')
                with tag('th'):
                    text('Product Name')    
                with tag('th'):
                    text('Quantity')
                with tag('th'):
                    text('Price')

            for i in range(len(orderedBooks)):
                with tag('tr'):
                    for key, attribute in (orderedBooks[i].items()):
                        with tag('th'):
                            text(attribute)


fileName = orderDate + "_" + orderNumber
f = open( fileName + ".html", "w")
f.write(doc.getvalue())