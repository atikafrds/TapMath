using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Item {

	public int ID;
	public String name;
	public int price;
	public int ownership;

	public Item(int ID, String name, int price, int ownership) {
		this.ID = ID;
		this.name = name;
		this.price = price;
		this.ownership = ownership;
	}

	public void setID(int ID) {
		this.ID = ID;
	}

	public void setName(String name) {
		this.name = name;
	}

	public void setPrice(int price) {
		this.price = price;
	}

	public void setOwnership(int ownership) {
		this.ownership = ownership;
	}
		
	public int getID() {
		return this.ID;
	}

	public String getName() {
		return this.name;
	}

	public int getPrice() {
		return this.price;
	}

	public int getOwnership() {
		return this.ownership;
	}
}
