# Модуль хранения данных
def make_contact(name, phone, email):
    return {"name": name.strip(), "phone": phone.strip(), "email": email.strip()}


def contact_to_str(contact):
    return f"{contact['name']} | {contact['phone']} | {contact['email']}"