from fpdf import FPDF

#zmienne wejsciowe

date = ''
orderNumber = ''
orderDate = ''
customerFullName = ''
customerStreet = ''
customerCity = ''
customerZip = ''
customerCountry = ''
refusalArgumentation = ''
additionalText = ''

#wczytanie danych do zmiennych

date = input()
orderNumber = input()
orderDate = input()
customerFullName = input()
customerStreet = input()
customerCity = input()
customerZip = input()
customerCountry = input()
refusalArgumentation = input()
additionalText = input()

#tworzenie PDFa

pdf = FPDF()

pdf.add_page()
effectivePageWidth = pdf.w - 2*pdf.l_margin
pdf.set_font("Arial", 'B', size=14)
pdf.cell(200,20, txt="The refusal of the order execution", ln=1, align="C")
pdf.set_font("Arial", size=10)
pdf.cell(200,5, txt="Gdansk, " + date + "      ", ln=1, align="R")
pdf.ln(10)
pdf.multi_cell(effectivePageWidth, 3.5, customerFullName, 0, 'L')
pdf.multi_cell(effectivePageWidth, 3.5, customerStreet, 0, 'L')
pdf.multi_cell(effectivePageWidth, 3.5, customerZip + ' ' + customerCity, 0, 'L')
pdf.multi_cell(effectivePageWidth, 5, customerCountry, 0, 'L')
pdf.cell(200,15, txt="Dear Customer,", ln=1, align="L")
pdf.multi_cell(effectivePageWidth, 4, "thank you very much for making the order number " + orderNumber + " day " + orderDate + ".", 0, 'L')
pdf.multi_cell(effectivePageWidth, 4, "Concurrently, we would like to inform you, that we can not realize your order, because " + refusalArgumentation + ".", 0, 'L')
if (len(additionalText) > 0):
    pdf.cell(500, 4, additionalText, ln=1, align="L")
pdf.cell(200, 4, "We apologize you for this situation.", ln=1, align="L")
pdf.ln(20)
pdf.cell(200,0.15, txt="Yours faithfully,        ", ln=1, align="R")
pdf.cell(200,10, txt="THE GREAT BOOKSTORE       ", ln=1, align="R")

pdf.output(orderNumber + "_refusal.pdf")