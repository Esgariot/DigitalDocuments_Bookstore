from odf.opendocument import *
from odf.style import Style, TextProperties, ParagraphProperties, ListLevelProperties, TabStop, TabStops
from odf.text import H, P, List, ListItem, ListStyle, ListLevelStyleNumber, ListLevelStyleBullet
from odf.table import Table, TableCell, TableColumn, TableRow, TableSource, TableHeaderColumns, TableHeaderRows
from odf.opendocument import Spreadsheet
from odf import teletype

import xmltodict

#XML data
orderNumber = ''
orderDate = ''
shippingAddress = ''
billingAddress = ''
orderedBooks = ''

with open('PurchaseOrderTemplate.xml') as fd:
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


#ODF

textdoc = OpenDocumentText()

# Creating different style used in the document
s = textdoc.styles

# For Level-1 Headings right
h1style = Style(name="CenterHeading 1", family="paragraph")
h1style.addElement(ParagraphProperties(attributes={"textalign": "right"}))
h1style.addElement(TextProperties(
    attributes={"fontsize": "18pt", "fontweight": "bold"}))

# For Level-2 Headings right
h2style = Style(name="CenterHeading 2", family="paragraph")
h2style.addElement(ParagraphProperties(attributes={"textalign": "right"}))
h2style.addElement(TextProperties(
    attributes={"fontsize": "15pt", "fontweight": "bold"}))

h3style = Style(name="LeftHeading 3", family="paragraph")
h3style.addElement(ParagraphProperties(attributes={"textalign": "left"}))
h3style.addElement(TextProperties(
    attributes={"fontsize": "12pt", "fontweight": "bold"}))

# For bold text
boldstyle = Style(name="Bold", family="text")
boldstyle.addElement(TextProperties(attributes={"fontweight": "bold"}))

# Justified style
justifystyle = Style(name="justified", family="paragraph")
justifystyle.addElement(ParagraphProperties(
    attributes={"textalign": "left"}))

# Basic style - text from letf to right
basicstyle = Style(name="left", family="paragraph")
basicstyle.addElement(ParagraphProperties(
    attributes={"textalign": "left"}))

# Register created styles to styleset
s.addElement(h1style)
s.addElement(h2style)
s.addElement(h3style)
s.addElement(boldstyle)
s.addElement(justifystyle)



# ---------ADDING TEXT TO DOCUMENT------------------


# Adding main heading
mymainheading_element = H(outlinelevel=1, stylename=h1style)
mymainheading_text = "PURCHASE ORDER"
teletype.addTextToElement(mymainheading_element, mymainheading_text)
textdoc.text.addElement(mymainheading_element)

# Adding second heading
mysecondheading_element = H(outlinelevel=1, stylename=h2style)
mysecondheading_text = 'DATE\t' + orderDate + '\n PO#\t' + orderNumber + '\n\n'
teletype.addTextToElement(mysecondheading_element, mysecondheading_text)
textdoc.text.addElement(mysecondheading_element)

# Adding a paragraph

for key, attribute in shippingAddress.items():
    if(key == '@Type'):
        paragraph_element = H(outlinelevel=1, stylename=h3style)
    else:
        paragraph_element = P(stylename=basicstyle)
    
    text = attribute
    teletype.addTextToElement(paragraph_element, text)
    textdoc.text.addElement(paragraph_element, text)

#Adding a gap between addresses
paragraph_element = P(stylename=basicstyle)
text = '\n'
teletype.addTextToElement(paragraph_element, text)
textdoc.text.addElement(paragraph_element, text)

for key, attribute in billingAddress.items():
    if(key == '@Type'):
        paragraph_element = H(outlinelevel=1, stylename=h3style)
    else:
        paragraph_element = P(stylename=basicstyle)
    
    text = attribute
    teletype.addTextToElement(paragraph_element, text)
    textdoc.text.addElement(paragraph_element, text)

#Adding a gap 
paragraph_element = P(stylename=basicstyle)
text = '\n\n'
teletype.addTextToElement(paragraph_element, text)
textdoc.text.addElement(paragraph_element, text)


# Table with ordered items

table = Table(name='Items')
trheader = TableRow()
tcheader = TableCell(valuetype='string', stylename=h3style)
tcheader.addElement(P(text='Product ID'))
trheader.addElement(tcheader)

tcheader = TableCell(valuetype='string', stylename=h3style)
tcheader.addElement(P(text='Product Name'))
trheader.addElement(tcheader)

tcheader = TableCell(valuetype='string', stylename=h3style)
tcheader.addElement(P(text='Quantity'))
trheader.addElement(tcheader)

tcheader = TableCell(valuetype='string', stylename=h3style)
tcheader.addElement(P(text='Price'))
trheader.addElement(tcheader)

table.addElement(trheader)

for i in range(len(orderedBooks)):
    tr = TableRow()
    for key, attribute in (orderedBooks[i].items()):
        tc = TableCell(valuetype='string')
        tc.addElement(P(text=attribute))
        tr.addElement(tc)
    table.addElement(tr)


textdoc.text.addElement(table)


textdoc.save(orderDate + '_' + orderNumber + '.odt')
