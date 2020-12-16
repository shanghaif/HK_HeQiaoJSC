<template>
  <div>
    <Card>
      <tables
        ref="tables"
        editable
        searchable
        :border="false"
        size="small"
        search-place="top"
        v-model="stores.role.data"
        :totalCount="stores.role.query.totalCount"
        :columns="stores.role.columns"
        @on-show="handleshow"
        @on-select="handleSelect"
        @on-selection-change="handleSelectionChange"
        @on-refresh="handleRefresh"
        :row-class-name="rowClsRender"
        @on-page-change="handlePageChanged"
        @on-page-size-change="handlePageSizeChanged">
        <div slot="search">
          <section class="dnc-toolbar-wrap">
            <Row :gutter="16">
              <Col span="16">
                <Form inline @submit.native.prevent>
                  <FormItem>
                    <Input
                      type="text"
                      search
                      :clearable="true"
                      v-model="stores.role.query.kw"
                      placeholder="输入关键字搜索..."
                      @on-search="handleSearchRole()">
                    </Input>
                  </FormItem>

                   <FormItem>
                    <DatePicker type="month" 
                    style="width: 180px;"
                    :value="stores.role.query.kwendtime" 
                    @on-change="stores.role.query.kwendtime=$event" 
                    format="yyyy-MM"
                    placeholder="选择时间" >
                    </DatePicker>
                  </FormItem>
                  <Button
                  v-can="'search'"
                  icon="md-search"
                  type="primary"
                  @click="handleSearchRole()"
                  title="查询">查询
                 </Button>
                </Form>
              </Col>
              <Col span="8" class="dnc-toolbar-btns">
                <ButtonGroup class="mr3">
                  <Button v-can="'export'" icon="md-refresh" title="刷新" @click="handleRefresh"></Button>
                </ButtonGroup>

                <Button
                  type="success"
                  @click="exportData"
                > {{"导出"}} </Button>

              </Col>
            </Row>
          </section>
        </div>
      </tables>
    </Card>

    <!--详情-->
    <Drawer
	    title="详情"
	    v-model="formMode2.opened"
	    width="400"
	    :mask-closable="false"
	    :mask="false"
	    :styles="styles">
        <Form :model="formMode2.fields" ref="详情"  label-position="left">
          <FormItem label="姓名">
              <Input v-model="formMode2.fields.declareName" :readonly="true"/>
          </FormItem>
          <FormItem label="部门">
              <Input v-model="formMode2.fields.declareDepartmentName" :readonly="true"/>
          </FormItem>
          <FormItem label="时间">
              <Input v-model="formMode2.fields.declareTime" :readonly="true"/>
          </FormItem>
          <FormItem label="加分项">
              <Input v-model="formMode2.fields.bonusPoint" :readonly="true"/>
          </FormItem>
          <FormItem label="加分分值">
              <Input v-model="formMode2.fields.plusScore" :readonly="true"/>
          </FormItem>
          <FormItem label="加分内容">
              <Input v-model="formMode2.fields.plusContent" :readonly="true"/>
          </FormItem>
          <FormItem label="减分项">
              <Input v-model="formMode2.fields.deduction" :readonly="true"/>
          </FormItem>
          <FormItem label="减分分值">
              <Input v-model="formMode2.fields.deductionScore" :readonly="true"/>
          </FormItem>
          <FormItem label="减分内容">
              <Input v-model="formMode2.fields.deductionContent" :readonly="true"/>
          </FormItem>
          <FormItem label="备注" label-position="top">
            <Input type="textarea" v-model="formMode2.fields.remark" :rows="4" :readonly="true" />
          </FormItem> 
          <FormItem label="创建时间">
              <Input v-model="formMode2.fields.establishTime" :readonly="true"/>
          </FormItem>
          <FormItem label="创建人">
              <Input v-model="formMode2.fields.establishName" :readonly="true"/>
          </FormItem>
        </Form>
	  </Drawer>

  </div>
</template>

<script>
import Tables from "_c/tables";
import config from '@/config';
import {
  collectGetRoleList,//查询数据列表
  loadRole,//显示数据单条数据
} from "@/api/performance/performanceDeclare";
export default {
  // url:"",
  name: "rbac_role_page",
  components: {
    Tables
  },
  data() {
    return {
      commands: {
        delete: { name: "delete", title: "删除" },
        recover: { name: "recover", title: "恢复" },
        forbidden: { name: "forbidden", title: "禁用" },
        normal: { name: "normal", title: "启用" }
      },

      accessoryas:"",
      //添加编辑
      formModel: {
        opened: false,
        title: "创建绩效申报",
        mode: "create",
        selection: [],
        fields: {
          id: "",
        },
        rules: {
            roleName: [
            {
              type: "string",
              required: true,
              message: "请输入名称",
              min: 2
            }
          ]
        }
      },
      //详情
      formMode2:{
        opened: false,
        title: "绩效申报详情",
        mode: "show",
        selection: [],
        fields: {
        id: "",
        },
      },

      stores: {
        role: {
          query: {
            totalCount: 0,
            pageSize: 20,
            currentPage: 1,
            kw: "",
            kwendtime:"",
            isDeleted: 0,
            status: -1,
            sort: [
              {
                direct: "DESC",
                field: "DeclareTime"
              }
            ]
          },
          sources: {
            isDeletedSources: [
              { value: -1, text: "全部" },
              { value: 0, text: "正常" },
              { value: 1, text: "已删" }
            ],
            statusSources: [
              { value: -1, text: "全部" },
              { value: 0, text: "禁用" },
              { value: 1, text: "正常" }
            ],
            statusFormSources: [
              { value: 0, text: "禁用" },
              { value: 1, text: "正常" }
            ]
          },
          //绑定数据
          columns: [
            { type: "selection", width: 50, key: "handle" },
            { title: "姓名", key: "declareName",ellipsis: true,tooltip: true},
            { title: "部门",ellipsis: true,tooltip: true,key: "declareDepartment"},
            { title: "时间", key: "declareTime" },
            { title: "加分项", key: "bonusPoint" ,ellipsis: true,tooltip: true},
            { title: "加分分值", key: "plusScore" },
            { title: "加分内容", key: "plusContent" ,ellipsis: true,tooltip: true},
            { title: "减分项", key: "deduction" ,ellipsis: true,tooltip: true},
            { title: "减分分值", key: "deductionScore" },
            { title: "减分内容", key: "deductionContent" ,ellipsis: true,tooltip: true},
          ],
          data: []
        }
      },
      styles: {
        height: "calc(100% - 55px)",
        overflow: "auto",
        paddingBottom: "53px",
        position: "static"
      }
    };
  },
  computed: {
    formTitle() {
      if (this.formModel.mode === "create") {
        return "创建绩效申报";
      }
      if (this.formModel.mode === "edit") {
        return "编辑绩效申报";
      }
      return "";
    },
    selectedRows() {
      return this.formModel.selection;
    },
    selectedRowsId() {
      return this.formModel.selection.map(x => x.departmentDeclareUuid);
    }
  },
  methods: {
  
    //导出选择
    compareObject (obj1, obj2) {
        var attrs1 = Object.keys(obj1)
        var attrs2 = Object.keys(obj2)
        if (attrs1.length !== attrs2.length) {
          return false
        }
        for (var j = 0; j < attrs1.length; j++) {
          var attrNames = attrs1[j]
          if (obj1[attrNames] !== obj2[attrNames]) {
            return false
          }
        }
        return true
      },


    //导出
    exportData(type) {
          console.log(11111111111);
          console.log(this.formModel.selection);
      if (this.formModel.selection == null || this.formModel.selection == "") {
        //  console.log(this.stores.role.columns);
          console.log(222222);
        this.$refs.tables.exportCsv({
          filename: "绩效信息列表",
          columns: this.stores.role.columns.filter((col, index) => index > 0),
          data: this.stores.role.data.map(k => {
            if (k.declareName != null) k.declareName = "\t" + k.declareName;
            if (k.declareDepartment != null) k.declareDepartment = "\t" + k.declareDepartment;
            if (k.declareTime != null) k.declareTime = "\t" + k.declareTime;
            if (k.bonusPoint != null) k.bonusPoint = "\t" + k.bonusPoint;
            if (k.plusScore != null) k.plusScore = "\t" + k.plusScore;
            if (k.plusContent != null) k.plusContent = "\t" + k.plusContent;
            if (k.deduction != null) k.deduction = "\t" + k.deduction;
            if (k.deductionScore != null) k.deductionScore = "\t" + k.deductionScore;
            if (k.deductionContent != null) k.deductionContent = "\t" + k.deductionContent;
            return k;
          })
        });
      } else {
        this.$refs.tables.exportCsv({
          filename: "绩效信息列表",
          columns: this.stores.role.columns.filter((col, index) => index > 0),
          data: this.stores.role.data
            .filter((data, index) => {
              for (let i = 0; i < this.formModel.selection.length; i++) {
                if (this.compareObject(data, this.formModel.selection[i])) {
                  return true;
                }
              }
              return false;
            })
            .map(k => {
            if (k.declareName != null) k.declareName = "\t" + k.declareName;
            if (k.declareDepartment != null) k.declareDepartment = "\t" + k.declareDepartment;
            if (k.declareTime != null) k.declareTime = "\t" + k.declareTime;
            if (k.bonusPoint != null) k.bonusPoint = "\t" + k.bonusPoint;
            if (k.plusScore != null) k.plusScore = "\t" + k.plusScore;
            if (k.plusContent != null) k.plusContent = "\t" + k.plusContent;
            if (k.deduction != null) k.deduction = "\t" + k.deduction;
            if (k.deductionScore != null) k.deductionScore = "\t" + k.deductionScore;
            if (k.deductionContent != null) k.deductionContent = "\t" + k.deductionContent;
            return k;
            })
        });
      }
    },

    //查询数据列表
    loadRoleList() {
      collectGetRoleList(this.stores.role.query).then(res => {
        this.stores.role.data = res.data.data;
			    // console.log(res.data.data);
        this.stores.role.query.totalCount = res.data.totalCount;
      });
    },
    //详情绩效申报页面
    handleshow(params) {
      this.handleSwitchFormModeToShow();
      this.doLoadShow(params.row.departmentDeclareUuid);
    },
    //详情绩效申报页面
    handleSwitchFormModeToShow() {
      this.formMode2.mode = "show";
      this.handleOpenFormShow();
    },
    //打开详情绩效申报右侧导航
    handleOpenFormShow() {
      this.formMode2.opened = true;
    },
    //详情绑定数据
    doLoadShow(departmentDeclareUuid) {
      loadRole({ guid: departmentDeclareUuid}).then(res => {
        this.formMode2.fields = res.data.data;
       this.accessoryas=this.formMode2.fields.accessory;
      });
    },

    handleSelect(selection, row) {},
    handleSelectionChange(selection) {
      this.formModel.selection = selection;
    },
    //刷新
    handleRefresh() {
      this.loadRoleList();
    },

    //批量操作
    doBatchCommand(command) {
      batchCommand({
        command: command,
        ids: this.selectedRowsId.join(",")
      }).then(res => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.loadRoleList();
          this.formModel.selection=[];
        } else {
          this.$Message.warning(res.data.message);
        }
        this.$Modal.remove();
      });
    },
    //搜索
    handleSearchRole() {
      this.loadRoleList();
    },
    rowClsRender(row, index) {
      if (row.isDeleted) {
        return "table-row-disabled";
      }
      return "";
    },
    //分页
    handlePageChanged(page) {
      this.stores.role.query.currentPage = page;
      this.loadRoleList();
    },
    handlePageSizeChanged(pageSize) {
      this.stores.role.query.pageSize = pageSize;
      this.loadRoleList();
    }
  },
  mounted() {
    this.loadRoleList();
  }
};
</script>
