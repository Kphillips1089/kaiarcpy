# -*- coding: utf-8 -*-
"""
/***************************************************************************
 MarketShare
                                 A QGIS plugin
 find market share
                              -------------------
        begin                : 2018-03-22
        git sha              : $Format:%H$
        copyright            : (C) 2018 by market
        email                : market@bigmarket.com
 ***************************************************************************/

/***************************************************************************
 *                                                                         *
 *   This program is free software; you can redistribute it and/or modify  *
 *   it under the terms of the GNU General Public License as published by  *
 *   the Free Software Foundation; either version 2 of the License, or     *
 *   (at your option) any later version.                                   *
 *                                                                         *
 ***************************************************************************/
"""
from PyQt4.QtCore import QSettings, QTranslator, qVersion, QCoreApplication
from PyQt4.QtGui import QAction, QIcon
# Initialize Qt resources from file resources.py
import resources
# Import the code for the dialog
from market_share_dialog import MarketShareDialog
import os.path
from PyQt4.QtGui import QFileDialog, QToolBar
from os import listdir, getcwd
from qgis.core import *
from qgis.gui import *
from qgis.utils import *
from PyQt4.QtCore import *
from PyQt4.QtGui import *
import processing
import os


class MarketShare:
    """QGIS Plugin Implementation."""

    def __init__(self, iface):
        """Constructor.

        :param iface: An interface instance that will be passed to this class
            which provides the hook by which you can manipulate the QGIS
            application at run time.
        :type iface: QgisInterface
        """
        # Save reference to the QGIS interface
        self.iface = iface
        # initialize plugin directory
        self.plugin_dir = os.path.dirname(__file__)
        # initialize locale
        locale = QSettings().value('locale/userLocale')[0:2]
        locale_path = os.path.join(
            self.plugin_dir,
            'i18n',
            'MarketShare_{}.qm'.format(locale))

        if os.path.exists(locale_path):
            self.translator = QTranslator()
            self.translator.load(locale_path)

            if qVersion() > '4.3.3':
                QCoreApplication.installTranslator(self.translator)


        # Declare instance attributes
        self.actions = []
        self.menu = self.tr(u'&market share')
        # TODO: We are going to let the user set this up in a future iteration
        self.toolbar = self.iface.addToolBar(u'MarketShare 1 toolbar')
        self.toolbar.setObjectName(u'MarketShare')

        self.work_path = getcwd()

    # noinspection PyMethodMayBeStatic
    def tr(self, message):
        """Get the translation for a string using Qt translation API.

        We implement this ourselves since we do not inherit QObject.

        :param message: String for translation.
        :type message: str, QString

        :returns: Translated version of message.
        :rtype: QString
        """
        # noinspection PyTypeChecker,PyArgumentList,PyCallByClass
        return QCoreApplication.translate('MarketShare', message)


    def add_action(
        self,
        icon_path,
        text,
        callback,
        enabled_flag=True,
        add_to_menu=True,
        add_to_toolbar=True,
        status_tip=None,
        whats_this=None,
        parent=None):
        """Add a toolbar icon to the toolbar.

        :param icon_path: Path to the icon for this action. Can be a resource
            path (e.g. ':/plugins/foo/bar.png') or a normal file system path.
        :type icon_path: str

        :param text: Text that should be shown in menu items for this action.
        :type text: str

        :param callback: Function to be called when the action is triggered.
        :type callback: function

        :param enabled_flag: A flag indicating if the action should be enabled
            by default. Defaults to True.
        :type enabled_flag: bool

        :param add_to_menu: Flag indicating whether the action should also
            be added to the menu. Defaults to True.
        :type add_to_menu: bool

        :param add_to_toolbar: Flag indicating whether the action should also
            be added to the toolbar. Defaults to True.
        :type add_to_toolbar: bool

        :param status_tip: Optional text to show in a popup when mouse pointer
            hovers over the action.
        :type status_tip: str

        :param parent: Parent widget for the new action. Defaults None.
        :type parent: QWidget

        :param whats_this: Optional text to show in the status bar when the
            mouse pointer hovers over the action.

        :returns: The action that was created. Note that the action is also
            added to self.actions list.
        :rtype: QAction
        """

        # Create the dialog (after translation) and keep reference
        self.dlg = MarketShareDialog()
        # clear the line edit widget, and set the pushbutton callback
        self.dlg.lineEdit.clear()
        self.dlg.pushButton.clicked.connect(self.choose_folder)

        icon = QIcon(icon_path)
        action = QAction(icon, text, parent)
        action.triggered.connect(callback)
        action.setEnabled(enabled_flag)

        if status_tip is not None:
            action.setStatusTip(status_tip)

        if whats_this is not None:
            action.setWhatsThis(whats_this)

        if add_to_toolbar:
            self.toolbar.addAction(action)

        if add_to_menu:
            self.iface.addPluginToMenu(
                self.menu,
                action)

        self.actions.append(action)

        return action

    def choose_folder(self):
        work_path1 = QFileDialog.getExistingDirectory(self.dlg, "Chose data folder", self.work_path)
        if work_path1:
            self.work_path = work_path1
            self.dlg.lineEdit.setText(self.work_path)
            files = listdir(self.work_path)
            shpfiles = []
            tables = []
            for f in files:
                if f[-4:] == '.shp':
                    shpfiles.append(f)
            for t in files:
                if t[-4:] == '.dbf':
                    tables.append(t)
            self.dlg.comboBox.clear()
            self.dlg.comboBox.addItems(shpfiles)
            self.dlg.comboBox_2.clear()
            self.dlg.comboBox_2.addItems(shpfiles)
            self.dlg.comboBox_3.clear()
            self.dlg.comboBox_3.addItems(tables)
            self.dlg.comboBox_4.clear()
            self.dlg.comboBox_4.addItems(shpfiles)
            self.dlg.lineEdit_2.setText(work_path1)

    def initGui(self):
        """Create the menu entries and toolbar icons inside the QGIS GUI."""

        icon_path = ':/plugins/MarketShare/icon.png'
        self.add_action(
            icon_path,
            text=self.tr(u'Test for market shares'),
            callback=self.run,
            parent=self.iface.mainWindow())


    def unload(self):
        """Removes the plugin menu item and icon from QGIS GUI."""
        for action in self.actions:
            self.iface.removePluginMenu(
                self.tr(u'&market share'),
                action)
            self.iface.removeToolBarIcon(action)
        # remove the toolbar
        del self.toolbar


    def run(self):
        """Run method that performs all the real work"""
        # show the dialog
        self.dlg.show()
        # Run the dialog event loop
        result = self.dlg.exec_()
        # See if OK was pressed
        if result:
            # Do something useful here - delete the line containing pass and
            # substitute with your code.
            selectedLayerIndex = self.dlg.comboBox.currentText()
            filename = self.work_path + '/' + selectedLayerIndex
            msg = 'The input shapefile will be {}'.format(filename)
            path = str(filename)
            layer = iface.addVectorLayer(path, '', "ogr")
            self.iface.messageBar().pushInfo('Open', msg)

            selectedLayerIndex2 = self.dlg.comboBox_2.currentText()
            selectedLayerIndex3 = self.dlg.comboBox_3.currentText()
            selectedLayerIndex4 = self.dlg.comboBox_4.currentText()
            filename2 = self.work_path + '/' + selectedLayerIndex2
            filename3 = self.work_path + '/' + selectedLayerIndex3
            filename4 = self.work_path + '/' + selectedLayerIndex4
            path2 = str(filename2)
            path3 = str(filename3)
            path4 = str(filename4)
            mainworkpath = self.work_path + '/'

            trtshp = path4
            layer = QgsVectorLayer(trtshp, 'Franklin tracts', 'ogr')

            shpname = path4
            dbfname = path3
            outshpname = mainworkpath

            layer = QgsVectorLayer(shpname, 'Tracts', 'ogr')
            layer.isValid()

            table = QgsVectorLayer(dbfname, 'Population', 'ogr')
            table.isValid()

            info = QgsVectorJoinInfo()
            info.joinLayerId = table.id()
            info.joinFieldName = 'GEO_ID'
            info.targetFieldName = 'STFID2'
            info.setJoinFieldNamesSubset(['P001001'])
            info.memoryCache = True

            layer.addJoin(info)
            QgsVectorFileWriter.writeAsVectorFormat(layer, outshpname, 'CP120', None, 'ESRI Shapefile')

            layer_joined = QgsVectorLayer(outshpname, 'Joined', 'ogr')
            layer_joined.isValid()
            layer.dataProvider().fields().count()
            layer_joined.dataProvider().fields().count()

            shp1 = path4
            shp2 = path
            layer_joined = QgsVectorLayer(shp1, 'Franklin tracts', 'ogr')
            layer2 = QgsVectorLayer(shp2, 'Public libraries', 'ogr')
            registry = QgsMapLayerRegistry.instance()
            registry.addMapLayers([layer2, layer_joined])

            shpname = 'H:/qgis2/Data/trt00_shp.shp'
            layer = QgsVectorLayer(shpname, 'county', 'ogr')
            pointLayer = path
            outshp = 'H:/qgis2/Data/thiessen'
            extnt = layer.extent().toString()
            extent = extnt.replace(":", ",").replace(" ", "")
            bbox = extent
            processing.runandload("grass7:v.voronoi", pointLayer, False, False, bbox, -1, 0.0001, 0, outshp)

            processing.runandload('qgis:intersection', shp1, "Voronoi diagram", True, "intersect")
            processing.runalg('qgis:addfieldtoattributestable', "Intersection", "AreaNew", 1, 20, 0,
                              mainworkpath + "shp3")

            processing.runalg('qgis.fieldcalculator', "shp3", "AreaNew", 1, 20, 0, )