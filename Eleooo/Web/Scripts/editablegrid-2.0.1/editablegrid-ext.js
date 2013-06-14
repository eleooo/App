EditableGrid.gridList = {};
EditableGrid.prototype.deletedData = [];
EditableGrid.prototype.newAddedRowCount = 0;
EditableGrid.prototype.maxAllowRow = -1;
EditableGrid.prototype.__processColumns = EditableGrid.prototype.processColumns;
EditableGrid.prototype.actionColName = "";
EditableGrid.prototype.processColumns = function () {
    EditableGrid.gridList[this.name] = this;
    if (typeof (this.actionColName) == 'string' && this.actionColName != '') {
        this.setCellRenderer(this.actionColName, new CellRenderer({ render: function (cell, value) {
            cell.innerHTML = "<a onclick=\"if (confirm(unescape('%u786E%u5B9A%u8981%u5220%u9664%u5417%3F'))) { EditableGrid.gridList." + this.editablegrid.name + ".remove(" + cell.rowIndex + "); } \" style=\"cursor:pointer\">" +
            "<img src=\"/Scripts/editablegrid-2.0.1/images/delete.png\" border=\"0\" alt=\"delete\" title=\"Delete row\"/></a>";
        }
        }));
    }
    this.__processColumns();
};
EditableGrid.prototype.onLoaded = function () { };
EditableGrid.prototype.tableLoaded = function () {
    this.deletedData = [];
    this.newAddedRowCount = 0;
    if (typeof (this.currentContainerid) == 'string' && this.currentContainerid != "")
        this.refreshGrid();
    debugger;
    this.onLoaded();
};
EditableGrid.prototype.__remove = EditableGrid.prototype.remove;
EditableGrid.prototype.remove = function (rowIndex) {
    var isNew = this.getRowAttribute(rowIndex, 'isNew') || false;
    if (!isNew)
        this.deletedData.push({ id: this.getRowId(rowIndex) });
    this.__remove(rowIndex);
};
EditableGrid.prototype.__insert = EditableGrid.prototype._insert;
EditableGrid.prototype._insert = function (rowIndex, offset, rowId, cellValues, rowAttributes, dontSort) {
    if (this.maxAllowRow && this.maxAllowRow == this.data.length) {
        alert(unescape("%u5DF2%u7ECF%u8FBE%u5230%u6700%u5927%u884C%u6570%u9650%u5236%21"));
        return;
    }
    if (rowId == null)
        rowId = this.getNewAddedRowIndex();
    else if (typeof (rowId) != 'number') {
        dontSort = rowAttributes;
        rowAttributes = cellValues;
        cellValues = rowId;
        rowId = this.getNewAddedRowIndex();
    }
    this.__insert(rowIndex, offset, rowId, cellValues, rowAttributes, dontSort);
    this.newAddedRowCount++;    
    this.setRowAttribute(rowIndex, 'isNew', true);
};
EditableGrid.prototype.onChanged = function (rowIndex, columnIndex, oldValue, newValue, row) { };
EditableGrid.prototype.modelChanged = function (rowIndex, columnIndex, oldValue, newValue, row) {
    this.setRowAttribute(rowIndex, 'isModified', true);
    if (this.onChanged)
        this.onChanged(rowIndex, columnIndex, oldValue, newValue, row);
};
EditableGrid.prototype.getChangedJSON = function () {
    return JSON.stringify(this.getChangedData());
}
EditableGrid.prototype.getChangedData = function () {
    return {
        'deleted': this.deletedData,
        'newAdded': this.getNewAddedValues(),
        'modified': this.getModifiedValues()
    };
};
EditableGrid.prototype.getNewAddedValues = function () {
    var result = [];
    for (i = 0; i < this.data.length; i++) {
        if (this.data[i]['isNew'] == true && this.data[i]['isModified'] == true)
            result.push(this.getRowValues(i));
    }
    return result;
};
EditableGrid.prototype.getModifiedValues = function () {
    var result = [];
    for (i = 0; i < this.data.length; i++) {
        if (this.data[i]['isNew'] != true && this.data[i]['isModified'] == true) {
            result.push(this.getRowValues(i));
        }
    }
    return result;
};
EditableGrid.prototype.getNewAddedRowIndex = function () {
    return -(this.newAddedRowCount + 1);
};
EditableGrid.prototype.addRow = function (rowId, cellValues, rowAttributes, dontSort) {
    rowId = rowId || this.getNewAddedRowIndex();
    cellValues = cellValues || this.getNullCelVal();
    dontSort = dontSort === false ? dontSort : true;
    var rowIndex = this.data.length == 0 ? 0 : this.data.length;
    return this._insert(rowIndex, 1, rowId, cellValues, rowAttributes, dontSort);
};
EditableGrid.prototype.getNullCelVal = function () {
    var celVal = {};
    for (var c = 0; c < this.columns.length; c++) {
        celVal[this.columns[c].name] = "";
    }
    return celVal;
};